﻿using AutoMapper;
using EX.Model.DbLayer;
using EX.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EX.Model.Repositories
{
    public class VisitorRepositoryDTO
    {
        IMapper mapper;
        VisitorRepository visitorRepository;

        public VisitorRepositoryDTO()
        {
            visitorRepository = new VisitorRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Visitor, VisitorDTO>();
                cfg.CreateMap<VisitorDTO, Visitor>();
            });
            mapper = config.CreateMapper();
        }
       
        public VisitorDTO AddOrUpdateVisitor(VisitorDTO visitorDTO)
        {
            Visitor visitor = mapper.Map<Visitor>(visitorDTO);
            if (visitor != null)
            {
                visitorDTO = mapper.Map<VisitorDTO>(visitorRepository.AddOrUpdateVisitor(visitor));
            }
            return visitorDTO;
        }

        public IEnumerable<VisitorDTO> GetAllVisitors()
        {
            var result = visitorRepository.GetAllVisitors().Select(s => mapper.Map<VisitorDTO>(s)).ToList();
            return result;
        }

        public VisitorDTO GetVisitorById(int Id)
        {
            return mapper.Map<VisitorDTO>(visitorRepository.GetVisitorById(Id));
        }

        public bool RemoveVisitorDTO(VisitorDTO visitorDTO)
        {
            bool result;
            try
            {
                visitorRepository.RemoveVisitor(mapper.Map<Visitor>(visitorDTO));
                result = true;
            }
            catch { result = false; }
            return result;
        }

        public bool RemoveVisitorById(int Id)
        {
            bool result;
            try
            {
                visitorRepository.RemoveVisitorById(Id);
                result = true;
            }
            catch { result = false; }
            return result;
        }
    }
}
