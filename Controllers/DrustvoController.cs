using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeeOrganizer.Data;
using BeeOrganizer.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeeOrganizer.Controllers
{
    public class DrustvoController : Controller
    {
        private readonly Cebelarstvo _context;

        public DrustvoController(Cebelarstvo context)
        {
            _context = context;
        }
    }
}