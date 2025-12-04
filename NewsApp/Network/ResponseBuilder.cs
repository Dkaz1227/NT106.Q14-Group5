using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Models;
using NewsApp.Models.Enums;


namespace NewsApp.Network
{
    public static class ResponseBuilder
    {
        // ========================
        //  HÀM CHUNG
        // ========================
        private static string Build<T>(MessageType type, bool status, string message, T data)
        {
            var response = new ServerResponse<T>
            {
                Type = type,
                Status = status,
                Message = message,
                Data = data
            };

            return JsonConvert.SerializeObject(response);
        }

        // ========================
        //  SUCCESS RESPONSE
        // ========================
        public static string LoginSuccess(UserInfo user)
            => Build(MessageType.Login, true, "OK", user);

        public static string RegisterSuccess(UserInfo user)
            => Build(MessageType.Register, true, "OK", user);

        public static string GetPostSuccess(List<PostInfo> posts)
            => Build(MessageType.GetPost, true, "OK", posts);

        public static string CreatePostSuccess(PostInfo post)
            => Build(MessageType.CreatePost, true, "OK", post);

        public static string CommentSuccess(CommentInfo comment)
            => Build(MessageType.Comment, true, "OK", comment);

        public static string ApproveSuccess(bool isApproved)
            => Build(MessageType.ApprovePost, true, "OK", isApproved);

        // ========================
        //  ERROR RESPONSE
        // ========================
        public static string LoginError(string error)
            => Build<object>(MessageType.Login, false, error, null);

        public static string RegisterError(string error)
            => Build<object>(MessageType.Register, false, error, null);

        public static string GetPostError(string error)
            => Build<object>(MessageType.GetPost, false, error, null);

        public static string CreatePostError(string error)
            => Build<object>(MessageType.CreatePost, false, error, null);

        public static string CommentError(string error)
            => Build<object>(MessageType.Comment, false, error, null);

        public static string ApproveError(string error)
            => Build<object>(MessageType.ApprovePost, false, error, null);
    }
}
