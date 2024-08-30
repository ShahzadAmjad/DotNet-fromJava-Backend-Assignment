using Backend_Assignment.Models;

namespace Backend_Assignment.Interfaces
{
    public interface IBlogPost
    {
        Task<ResponseModel> CreateBlogPost(BlogPost BlogPost);
        Task<BlogPost> getBlogPostById(int id);
        Task<List<BlogPost>> getAllBlogPosts();
        Task<ResponseModel> deleteBlogPost(int BlogPostId);
        Task<ResponseModel> updateBlogPost(BlogPost BlogPost);
    }
}
