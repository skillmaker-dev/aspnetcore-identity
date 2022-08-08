﻿using System.ComponentModel.DataAnnotations;

namespace IdentityByExamples.Models
{
    public class TwoStepModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
