using System;
namespace MusicPlayer.Model
{
    public class TrackModel
    {
        public string Genre { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Duration { get; set; }
    }
}