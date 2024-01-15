using AutoMapper;
using TreeAndJournal.Application.Journal.GetItem;
using TreeAndJournal.Application.Tree.GetTree;
using TreeAndJournal.Domain;

namespace TreeAndJournal.Application.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<JournalItem, JournalDto>()
                .ForMember(target => target.Text, source => source.MapFrom(x => $"Request Path={x.Path}," +
                $" Query params={x.QueryParams}, Body params={x.BodyParams}," +
                $" StackTrace={x.StackTrace} "));

            CreateMap<Node, NodeDto>();
        }
    }
}
