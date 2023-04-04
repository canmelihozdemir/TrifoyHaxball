using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<PlayerSaveDto, Player>();
            CreateMap<PlayerUpdateDto, Player>();
            CreateMap<PlayerRank, PlayerRankDto>().ReverseMap();
            CreateMap<Player, PlayerWithRankDto>();
            CreateMap<PlayerLoginDto, Player>();

        }
    }
}
