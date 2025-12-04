using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Network
{
    public class ResponseHandler
    {
        public void Handle(string json)
        {
            ClientRequest? baseReq;

            try
            {
                baseReq = JsonConvert.DeserializeObject<ClientRequest>(json);
            }
            catch
            {
                Console.WriteLine("Invalid JSON from client");
                return;
            }

            if (baseReq == null)
            {
                Console.WriteLine("Null request");
                return;
            }

            switch (baseReq.Type)
            {
                case MessageType.Login:
                    HandleLogin(JsonConvert.DeserializeObject<LoginRequest>(json));
                    break;

                case MessageType.Register:
                    HandleRegister(JsonConvert.DeserializeObject<RegisterRequest>(json));
                    break;

                case MessageType.GetPost:
                    HandleGetPost(JsonConvert.DeserializeObject<GetPostRequest>(json));
                    break;

                case MessageType.CreatePost:
                    HandleCreatePost(JsonConvert.DeserializeObject<CreatePostRequest>(json));
                    break;

                case MessageType.Comment:
                    HandleComment(JsonConvert.DeserializeObject<CommentRequest>(json));
                    break;

                case MessageType.ApprovePost:
                    HandleApprove(JsonConvert.DeserializeObject<ApprovePostRequest>(json));
                    break;

                default:
                    Console.WriteLine("Unknown message type");
                    break;
            }
        }

        // ============================================
        // IMPLEMENT từng handler
        // ============================================

        private void HandleLogin(LoginRequest? req)
        {
            if (req == null)
            {
                Console.WriteLine("Invalid LoginRequest");
                return;
            }

            Console.WriteLine($"Login: {req.Username}");
        }

        private void HandleRegister(RegisterRequest? req)
        {
            if (req == null)
            {
                Console.WriteLine("Invalid RegisterRequest");
                return;
            }

            Console.WriteLine($"Register: {req.Username}");
        }

        private void HandleGetPost(GetPostRequest? req)
        {
            Console.WriteLine("GetPost");
        }

        private void HandleCreatePost(CreatePostRequest? req)
        {
            Console.WriteLine($"CreatePost: {req?.Content}");
        }

        private void HandleComment(CommentRequest? req)
        {
            Console.WriteLine($"Comment on #{req?.PostId}");
        }

        private void HandleApprove(ApprovePostRequest? req)
        {
            Console.WriteLine($"Approve post #{req?.PostId}");
        }
    }
}
