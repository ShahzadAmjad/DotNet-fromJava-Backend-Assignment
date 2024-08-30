using Backend_Assignment.DBContext;
using Backend_Assignment.Interfaces;
using Backend_Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Assignment.Repository
{
    public class BlogPostRepo : IBlogPost
    {
        private readonly BlogPostContext _dbContext;
        public BlogPostRepo(BlogPostContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseModel> CreateBlogPost(BlogPost BlogPost)
        {
            ResponseModel model = new ResponseModel();

            try
            {

                var result = await _dbContext.BlogPosts.AddAsync(BlogPost);
                await _dbContext.SaveChangesAsync();

                model.Data = result.Entity;
                model.Messsage = "Saved Successfully";
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }

            return model;
        }

        public async Task<ResponseModel> deleteBlogPost(int BlogPostId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var blogPost = await _dbContext.BlogPosts
                    .Where(x => x.Id == BlogPostId)
                    .FirstOrDefaultAsync();

                if (blogPost != null)
                {
                    _dbContext.BlogPosts.Remove(blogPost); // Hard delete the record
                    await _dbContext.SaveChangesAsync();  // Use async SaveChanges

                    model.Data = null; // Since the record is deleted, there's no data to return
                    model.Messsage = "Deleted successfully";
                    model.IsSuccess = true;
                }
                else
                {
                    model.Messsage = "Blog post not found";
                    model.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Something went wrong: " + ex.Message;
            }

            return model;
        }

        public async Task<List<BlogPost>> getAllBlogPosts()
        {
            try
            {
                return await _dbContext.BlogPosts.ToListAsync();
                   
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BlogPost> getBlogPostById(int id)
        {
            try
            {
                return await _dbContext.BlogPosts
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel> updateBlogPost(BlogPost ModifiedBlogPost)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var ExistingBlogPost = await _dbContext.BlogPosts                  
                    .Where(x => x.Id == ModifiedBlogPost.Id)
                    .FirstOrDefaultAsync();

                if (ExistingBlogPost != null)
                {
                    _dbContext.Entry(ExistingBlogPost).CurrentValues.SetValues(ModifiedBlogPost);
                    
                }
                await _dbContext.SaveChangesAsync();
                model.Data = ModifiedBlogPost;
                model.Messsage = "Saved Successfully";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Something went wrong" + ex.Message;
            }
            return model;
        }
    }
}
