using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class ValidateImageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
           // int maxContent = 1024 * 1024; //1 MB
            int maxContent = 500 * 1024; //1 MB
           // string[] sAllowedExt = new string[] { ".jpg", ".gif", ".png" };
            string[] sAllowedExt = new string[] { ".jpg"};
            var file = value as HttpPostedFileBase;
            if (file == null)
                return false;

            else if (!sAllowedExt.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload your image of type: " + string.Join(", ", sAllowedExt);
                return false;
            }
            else if (file.ContentLength > maxContent)
            {
                ErrorMessage = "Your image is too large, maximum allowed size is : " + (maxContent / 1024).ToString() + "KB";
                return false;
            }
            else
                return true;
        }
    }

    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value1)
        {
            int maxContent = 1024 * 1024; //1 MB
            
           // string[] sAllowedExt = new string[] { ".jpg", ".gif", ".png" };
            string[] sAllowedExt = new string[] { ".pdf" };
            var file1 = value1 as HttpPostedFileBase;
            if (file1 == null)
                return false;

            else if (!sAllowedExt.Contains(file1.FileName.Substring(file1.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload your file of type: " + string.Join(", ", sAllowedExt);
                return false;
            }
            else if (file1.ContentLength > maxContent)
            {
                ErrorMessage = "Your file is too large, maximum allowed size is : " + (maxContent / 1024).ToString() + "MB";
                return false;
            }
            else
                return true;
        }
    }
}