using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EducationalHelp.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext([NotNull] DbContextOptions options) : base(options)
        {
        }
    }
}