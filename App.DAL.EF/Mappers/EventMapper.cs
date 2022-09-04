﻿using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class EventMapper : BaseMapper<Event, Domain.Event>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
}