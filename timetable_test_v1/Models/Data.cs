using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace timetable_test_v1.Models
{
    public partial class Classroom : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId? Id { get; set; }

        [MapTo("building")]
        public string? Building { get; set; }

        [MapTo("name")]
        public string? Name { get; set; }

        [MapTo("tag")]
        public string? Tag { get; set; }
    }

    public partial class Timetable : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }
        [MapTo("classname")]
        public string? Classname { get; set; }
        [MapTo("classroom_id")]
        public ObjectId? ClassroomId { get; set; }
        [MapTo("course")]
        public string? Course { get; set; }
        [MapTo("course_schedule")]
        public Schedule? CourseSchedule { get; set; }
        [MapTo("instructor")]
        public string? Instructor { get; set; }
        [MapTo("status")]
        public string? Status { get; set; }
    }


    public partial class Schedule : IEmbeddedObject
    {
        [MapTo("class")]
        public string? Class { get; set; }
        [MapTo("date")]
        public string? Date { get; set; }
        [MapTo("time_end")]
        public string? TimeEnd { get; set; }
        [MapTo("time_str")]
        public string? TimeStr { get; set; }
    }

}
