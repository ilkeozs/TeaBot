using System;

namespace TeaBot.Commands.Other
{
    public class KissSystem
    {
        private string[] links = { "https://media0.giphy.com/media/G3va31oEEnIkM/giphy.gif?cid=6c09b952d8wq5fxtpuovke00b1gjt3up1a79uj2mbaihrwbf&ep=v1_gifs_search&rid=giphy.gif&ct=g", "https://i.pinimg.com/originals/a0/c3/fd/a0c3fd54de1066e3a7d3a05e4d932d58.gif", "https://www.icegif.com/wp-content/uploads/2022/10/icegif-1395.gif", "https://www.icegif.com/wp-content/uploads/2022/08/icegif-1224.gif", "https://gifdb.com/images/high/taichi-yaegashi-anime-kiss-dcgns4emesa0hy6a.gif" };

        public string SelectedLink { get; set; }

        public KissSystem()
        {
            var random = new Random();
            int linkIndex = random.Next(0, links.Length - 0);

            this.SelectedLink = links[linkIndex];
        }
    }
}