using CardTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CardTest.Web
{
    public class ImageLoader
    {
        private const string JsonUrl = "https://drive.google.com/uc?export=download&id=1peESxjVYKB6qwGA4AX46HdZn3DRPbwK5";


        public async Task<List<Sprite>> LoadImage(int pairAmount)
        {
            var json = await LoadJson();
            var data = GetImageDataFromJson(json);
            var result = await LoadSprites(data, pairAmount);

            return result;
        }

        private async Task<string> LoadJson()
        {
            using(UnityWebRequest request = UnityWebRequest.Get(JsonUrl))
            {
                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if(request.result == UnityWebRequest.Result.Success)
                {
                    return request.downloadHandler.text;
                }
                else
                {
                    return null;
                }
            }
        }
        
        private ImageData GetImageDataFromJson(string json)
        {
            if (json == null) return null;

            ImageData data = JsonUtility.FromJson<ImageData>(json);

            return data;
        }

        private async Task<List<Sprite>> LoadSprites(ImageData data, int pairAmount)
        {
            List<Sprite> cardImage = new();
            do
            {
                var imageUrl = data.Images[Random.Range(0, data.Images.Count)];
                using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
                {
                    var operation = request.SendWebRequest();
                    while (!operation.isDone)
                    {
                        await Task.Yield();
                    }

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Texture2D texture = DownloadHandlerTexture.GetContent(request);
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                        cardImage.Add(sprite);
                    }
                }

                data.Images.Remove(imageUrl);
                if (data.Images.Count == 0) break;
            } while (cardImage.Count != pairAmount);

            return cardImage;
        }
    }
}