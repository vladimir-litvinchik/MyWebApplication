using Newtonsoft.Json;

namespace MyWebApplication.Viewer.Entity
{
    /// <summary>
    /// RotatedPageEntity.
    /// </summary>
    public class RotatedPageEntity
    {
        [JsonProperty]
        private int pageNumber;

        [JsonProperty]
        private string angle;

        public void SetPageNumber(int number) {
            this.pageNumber = number;
        }

        public int GetPageNumber()
        {
            return this.pageNumber;
        }

        public void SetAngle(string angle)
        {
            this.angle = angle;
        }

        public string GetAngle()
        {
            return this.angle;
        }
    }
}