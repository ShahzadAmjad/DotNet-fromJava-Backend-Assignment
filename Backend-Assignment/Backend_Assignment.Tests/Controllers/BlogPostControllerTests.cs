using Backend_Assignment.Controllers;
using Backend_Assignment.Interfaces;
using Backend_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Assignment.Tests.Controllers
{
    [TestFixture] // NUnit attribute to define a test class
    public class BlogPostControllerTests
    {
        private Mock<IBlogPost> _mockBlogPostService;
        private BlogPostController _controller;

        [SetUp] // NUnit attribute to run before each test
        public void Setup()
        {
            _mockBlogPostService = new Mock<IBlogPost>();
            _controller = new BlogPostController(_mockBlogPostService.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkResult_WithListOfBlogPosts()
        {
            // Arrange
            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = 1, Title = "Test Post 1" },
                new BlogPost { Id = 2, Title = "Test Post 2" }
            };
            _mockBlogPostService.Setup(service => service.getAllBlogPosts())
                .ReturnsAsync(blogPosts);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<BlogPost>;
            ClassicAssert.AreEqual(2, returnValue.Count);
        }
        [Test]
        public async Task GetById_ReturnsOkResult_WithBlogPost()
        {
            // Arrange
            var blogPost = new BlogPost { Id = 1, Title = "Test Post" };
            _mockBlogPostService.Setup(service => service.getBlogPostById(1))
                .ReturnsAsync(blogPost);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returnValue = okResult.Value as BlogPost;
            ClassicAssert.AreEqual("Test Post", returnValue.Title);
        }
        [Test]
        public async Task Add_ReturnsOkResult_WithCreatedBlogPost()
        {
            // Arrange
            var newBlogPost = new BlogPost { Id = 0, Title = "New Post" };
            var response = new ResponseModel { IsSuccess = true, Data = newBlogPost };
            _mockBlogPostService.Setup(service => service.CreateBlogPost(newBlogPost))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Add(newBlogPost);

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returnValue = okResult.Value as ResponseModel;
            ClassicAssert.IsTrue(returnValue.IsSuccess);
            ClassicAssert.AreEqual("New Post", ((BlogPost)returnValue.Data).Title);
        }
        [Test]
        public async Task Update_ReturnsOkResult_WithUpdatedBlogPost()
        {
            // Arrange
            var updatedBlogPost = new BlogPost { Id = 1, Title = "Updated Post" };
            var response = new ResponseModel { IsSuccess = true, Data = updatedBlogPost };
            _mockBlogPostService.Setup(service => service.updateBlogPost(updatedBlogPost))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(updatedBlogPost);

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returnValue = okResult.Value as ResponseModel;
            ClassicAssert.IsTrue(returnValue.IsSuccess);
            ClassicAssert.AreEqual("Updated Post", ((BlogPost)returnValue.Data).Title);
        }
        [Test]
        public async Task Delete_ReturnsOkResult_WhenBlogPostDeleted()
        {
            // Arrange
            var response = new ResponseModel { IsSuccess = true };
            _mockBlogPostService.Setup(service => service.deleteBlogPost(1))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returnValue = okResult.Value as ResponseModel;
            ClassicAssert.IsTrue(returnValue.IsSuccess);
        }
    }
}
