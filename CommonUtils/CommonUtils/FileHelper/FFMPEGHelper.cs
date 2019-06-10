
namespace CommonUtils.FileHelper
{
    public static class FFMPEGHelper
    {

        /// <summary>
        /// 获取视频某一帧图片
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="oriVideoPath"></param>
        /// <param name="frameIndex"></param>
        /// <param name="thubWidth"></param>
        /// <param name="thubHeight"></param>
        /// <param name="thubImagePath"></param>
        public static void GetImageOfIndex(string ffmpegPath, string oriVideoPath, string frameIndex, string thubWidth, string thubHeight, string thubImagePath)
        {
            string command = string.Format("\"{0}\" -i \"{1}\" -ss {2} -vframes 1 -r 1 -ac 1 -ab 2 -s {3}*{4} -f image2 \"{5}\"",
                ffmpegPath, oriVideoPath, frameIndex, thubWidth, thubHeight, thubImagePath);
        }
    }
}
