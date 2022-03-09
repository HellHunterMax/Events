using Events.Core.Entities;

namespace Events.Core.Specifications.EventsSpecifications
{
    public class GetEventByIdIncludeAllSpecification : BaseSpecification<Event>
    {
        public GetEventByIdIncludeAllSpecification(Guid id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Office);
            AddInclude(x => x.Categories);
            AddInclude(x => x.Attendees);
        }
    }
}
