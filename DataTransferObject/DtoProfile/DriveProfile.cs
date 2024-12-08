using AutoMapper;
using DataTransferObject.RequestDto;
using DataTransferObject.ResponseDto;
using Entity.Layer;
using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DtoProfile
{
    public class DriveProfile: Profile
    {
        public DriveProfile()
        {
            CreateMap<ResponseTeam, Team>();
            CreateMap<Team,ResponseTeam>();

            CreateMap<ResponseProject, Project>();
            CreateMap<Project, ResponseProject>();

            CreateMap<ResponseReport, Report>();
            CreateMap<Report, ResponseReport>();



            CreateMap<RequestTeam, Team>();
            CreateMap<Team, RequestTeam>();

            CreateMap<RequestProject, Project>();
            CreateMap<Project, RequestProject>();

            CreateMap<RequestReport, Report>();
            CreateMap<Report, RequestReport>();
        }
    }
}
