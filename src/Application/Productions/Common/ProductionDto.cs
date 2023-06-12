﻿using AutoMapper;
using CheckflixApp.Application.Common.Mappings;
using CheckflixApp.Application.TodoLists.Queries.GetTodos;
using CheckflixApp.Domain.Entities;

namespace CheckflixApp.Application.Productions.Common;

public class ProductionDto : IMapFrom<Production>
{
    public string TmdbId { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public IEnumerable<string> Genres { get; set; } = new List<string>();

    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<Production, ProductionDto>()
    //        .ForMember(d => d.Genres, opt => opt.MapFrom(s => s.ProductionGenres.Select(x => x.)));
    //}
}