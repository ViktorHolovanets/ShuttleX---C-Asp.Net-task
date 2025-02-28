﻿using System.ComponentModel.DataAnnotations;

namespace ShuttleX_task_api.Helpers.Classes
{
    public class PaginationInfo
    {
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0.")]
        public int Page { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0.")]
        public int Size { get; set; }
    }
}
