﻿using System;
using System.Collections.Generic;
using System.Text;
using VOD.Database.Contexts;
using System.Linq;
using VOD.Common.Entities;

namespace VOD.Database.Migrations
{
   public class DbInitializer
    {   

        public static void RecreateDatabase(VODContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        public static void Initialize(VODContext context)
        {
            var email = "a@b.c";
            var adminRoleId = string.Empty;
            var userId = string.Empty;

            var description = string.Empty; 

            if (context.Users.Any(r => r.Email.Equals(email)))
                userId = context.Users.First(r => r.Email.Equals(email)).Id;

            if (!userId.Equals(string.Empty))
            {
                if (!context.Instructors.Any())
                {
                    var instructors = new List<Instructor>
                        {
                            new Instructor {
                              Name = "John Doe",
                              Description = "desc-test",//description.Substring(20, 50),
                              Thumbnail = "/images/Ice-Age-Scrat-icon.png"
                                            },
                            new Instructor {
                              Name = "Jane Doe",
                              Description = "desc-test", 
                               //description.Substring(30, 40),
                              Thumbnail = "/images/Ice-Age-Scrat-icon.png"
                                           }
                        };
                    context.Instructors.AddRange(instructors);
                    context.SaveChanges();
                }

                // Course
                if (!context.Courses.Any())
                {
                    var instructorId1 = context.Instructors.First().Id;
                    var instructorId2 = int.MinValue;
                    var instructor = context.Instructors.Skip(1).FirstOrDefault();
                    if (instructor != null) instructorId2 = instructor.Id;
                    else instructorId2 = instructorId1;
                    var courses = new List<Course>
                    {
                      new Course 
                      {
                           InstructorId = instructorId1,
                           Title = "Course 1",
                           Description = description,
                           ImageUrl = "/images/course1.jpg",
                           MarqueeImageUrl = "/images/laptop.jpg"
                      },
                      new Course 
                      {
                           InstructorId = instructorId2,
                           Title = "Course 2",
                           Description = description,
                           ImageUrl = "/images/course2.jpg",
                           MarqueeImageUrl = "/images/laptop.jpg"
                      },
                      new Course 
                      {
                           InstructorId = instructorId1,
                           Title = "Course 3",
                           Description = description,
                           ImageUrl = "/images/course3.jpg",
                           MarqueeImageUrl = "/images/laptop.jpg"
                      }
                    };
                    context.Courses.AddRange(courses);
                    context.SaveChanges();
                }

                //  

                var courseId1 = int.MinValue;
                var courseId2 = int.MinValue;
                var courseId3 = int.MinValue;
                if (context.Courses.Any())
                {
                    courseId1 = context.Courses.First().Id;
                    var course = context.Courses.Skip(1).FirstOrDefault();
                    if (course != null) courseId2 = course.Id;
                    course = context.Courses.Skip(2).FirstOrDefault();
                    if (course != null) courseId3 = course.Id;
                }


                if (!context.UserCourses.Any())
                {
                    if (!courseId1.Equals(int.MinValue))
                        context.UserCourses.Add(new UserCourse
                        { UserId = userId, CourseId = courseId1 });
                    if (!courseId2.Equals(int.MinValue))
                        context.UserCourses.Add(new UserCourse
                        { UserId = userId, CourseId = courseId2 });
                    if (!courseId3.Equals(int.MinValue))
                        context.UserCourses.Add(new UserCourse
                        { UserId = userId, CourseId = courseId3 });
                    context.SaveChanges();
                }


                // Modules

                if (!context.Modules.Any())
                {
                    var modules = new List<Module>
                    {
                        new Module { Course = context.Find<Course>(courseId1),
                             Title = "Module 1" },
                        new Module { Course = context.Find<Course>(courseId1),
                             Title = "Module 2" },
                        new Module { Course = context.Find<Course>(courseId2),
                             Title = "Module 3" }
                    };
                    context.Modules.AddRange(modules);
                    context.SaveChanges();
                }

            }

            // 14번 New Module
            var moduleId1 = int.MinValue;
            var moduleId2 = int.MinValue;
            var moduleId3 = int.MinValue;
            if (context.Modules.Any())
            {
                moduleId1 = context.Modules.First().Id;
                var module = context.Modules.Skip(1).FirstOrDefault();
                if (module != null) moduleId2 = module.Id;
                else moduleId2 = moduleId1;
                module = context.Modules.Skip(2).FirstOrDefault();
                if (module != null) moduleId3 = module.Id;
                else moduleId3 = moduleId1;
            }

            if (!context.Videos.Any())
            {
                int courseId1 = 0;
                int courseId2 = 0;
                var videos = new List<Video>
                 {
                   new Video { ModuleId = moduleId1, CourseId = courseId1,
                               Title = "Video 1 Title",
                               Description = "Video 1 Desc", //description.Substring(1, 35),
                               Duration = 50, Thumbnail = "/images/video1.jpg",
                               Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                             },
                   new Video { ModuleId = moduleId1, CourseId = courseId1,
                               Title = "Video 2 Title",
                               Description = "Video 2 Desc",  //description.Substring(5, 35),
                               Duration = 45, Thumbnail = "/images/video2.jpg",
                               Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                             },
                   new Video { ModuleId = moduleId1, CourseId = courseId1,
                               Title = "Video 3 Title",
                               Description = "Video 3 Desc",  //description.Substring(10, 35),
                               Duration = 41, Thumbnail = "/images/video3.jpg",
                               Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                             },
                   new Video { ModuleId = moduleId3, CourseId = courseId2,
                               Title = "Video 4 Title",
                               Description = "Video 4 Desc",  //description.Substring(15, 35),
                               Duration = 41, Thumbnail = "/images/video4.jpg",
                               Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                             },
                   new Video { ModuleId = moduleId2, CourseId = courseId1,
                               Title = "Video 5 Title",
                               Description = "Video 5 Desc",  //description.Substring(20, 35),
                               Duration = 42, Thumbnail = "/images/video5.jpg",
                               Url = "https://www.youtube.com/watch?v=BJFyzpBcaCY"
                             }
                   };
                context.Videos.AddRange(videos);
                context.SaveChanges();
            }


            //16
            if (!context.Downloads.Any())
            {
                int courseId1 = 0;
                int courseId2 = 0;
                var downloads = new List<Download>
                {
                 new Download{ModuleId = moduleId1, CourseId = courseId1,
                              Title = "ADO.NET 1 (PDF)", Url = "https://some-url" },
                 new Download{ModuleId = moduleId1, CourseId = courseId1,
                              Title = "ADO.NET 2 (PDF)", Url = "https://some-url" },
                 new Download{ModuleId = moduleId3, CourseId = courseId2,
                              Title = "ADO.NET 1 (PDF)", Url = "https://some-url" }
                };
                context.Downloads.AddRange(downloads);
                context.SaveChanges();
            }



        }



    }
}
