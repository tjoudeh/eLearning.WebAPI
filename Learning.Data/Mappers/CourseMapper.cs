using Learning.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Data.Mappers
{
    class CourseMapper : EntityTypeConfiguration<Course>
    {
        public CourseMapper()
        {
            this.ToTable("Courses");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).IsRequired();
            this.Property(c => c.Name).HasMaxLength(255);

            this.Property(c => c.Duration).IsRequired();

            this.Property(c => c.Description).IsOptional();
            this.Property(c => c.Description).HasMaxLength(1000);

            this.HasRequired(c => c.CourseSubject).WithMany().Map(s => s.MapKey("SubjectID"));
            this.HasRequired(c => c.CourseTutor).WithMany().Map(t => t.MapKey("TutorID"));

        }
    }
}
