﻿using FileBrowser.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileBrowserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllFiles()
        {
            return FileBrowserModel.GetAllFiles().ToArray();
        }

        [HttpGet("{path}")]
        public ActionResult<IEnumerable<string>> GetAllFiles(string path)
        {
            var result = FileBrowserModel.GetAllFiles(path);
            if (result == null)
                return BadRequest("Path doesn't exisit.");
            return result.ToArray();
        }

        [HttpPut]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            var result = await FileBrowserModel.UploadFile(file);
            if (!result)
                return BadRequest("Path is not valid.");

            return Ok();
        }

        [HttpPut("{path}")]
        public async Task<ActionResult> UploadFile(IFormFile file, string path)
        {
            var result = await FileBrowserModel.UploadFile(file, path);
            if (!result)
                return BadRequest("Path is not valid.");

            return Ok();
        }

        [HttpGet("Filter/{regex}")]
        public ActionResult<IEnumerable<string>> GetFilteredFiles(string regex)
        {
            var result = FileBrowserModel.GetFilteredFiles(regex);
            if (result == null)
                return BadRequest();
            return result.ToArray();
        }

        [HttpDelete("{regex}")]
        public ActionResult<IEnumerable<string>> DeleteFilteredFiles(string regex)
        {
            var result = FileBrowserModel.DeleteFilteredFiles(regex);
            if (result == null)
                return BadRequest();
            return result.ToArray();
        }
    }
}
