using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWasmGrpcBlog.Shared.Protos;

namespace BlazorWasmGrpcBlog.Server.Models
{
	// See (gavilanch3) https://www.youtube.com/watch?v=yJAf5fKpGO
	public class PostsTags2
    {
		public int PostId { get; set; }
		public int TagId { get; set; }
		public Post Post { get; set; }
		public Tag Tag { get; set; }
		public DateTimeOffset CreatedUtc { get; set; }
	}
}
