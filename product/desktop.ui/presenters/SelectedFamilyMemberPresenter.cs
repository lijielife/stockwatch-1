using System.Collections.Generic;
using System.Collections.ObjectModel;
using gorilla.utility;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class SelectedFamilyMemberPresenter : Observable<SelectedFamilyMemberPresenter>, Presenter, EventSubscriber<AddedNewFamilyMember>
    {
        PersonDetails selected_member;
        EventAggregator event_aggregator;
        Mapper mapper;
        ServiceBus bus;

        public SelectedFamilyMemberPresenter(EventAggregator event_aggregator, Mapper mapper, ServiceBus bus)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.event_aggregator = event_aggregator;
            family_members = new ObservableCollection<PersonDetails>();
        }

        public ICollection<PersonDetails> family_members { get; set; }

        public PersonDetails SelectedMember
        {
            get { return selected_member; }
            set
            {
                selected_member = value;
                update(x => x.SelectedMember);
                event_aggregator.publish(new SelectedFamilyMember {id = value.id});
            }
        }

        public void present()
        {
            bus.publish<FindAllFamily>();
        }

        public void notify(AddedNewFamilyMember message)
        {
            family_members.Add(mapper.map_from<AddedNewFamilyMember, PersonDetails>(message));
            update(x => x.family_members);
        }
    }

}