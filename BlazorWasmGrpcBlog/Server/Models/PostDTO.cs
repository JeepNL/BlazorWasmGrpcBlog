using BlazorWasmGrpcBlog.Shared.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmGrpcBlog.Shared.DTOs
{
    public class PostDTO
    {
		public int PostId { get; set; }
		public int AuthorId { get; set; }
		public string Title { get; set; }
		public string DateCreated { get; set; }
		public string PostStat { get; set; }
		public List<Tag> Tags { get; set; }
	}
}
