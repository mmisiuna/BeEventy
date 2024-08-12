import { Component } from '@angular/core';
import { EventService } from '../services/event.service';
import { EventCommunicationService } from '../services/event-communication.service';
@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent {
  constructor(
    private eventService: EventService,
    private eventCommunicationService: EventCommunicationService
  ) {}

  sortByDate(): void {
    this.eventCommunicationService.requestEventListRefresh(() => this.eventService.getSortedEventsByDate());
  }

  sortByVotes(): void {
    this.eventCommunicationService.requestEventListRefresh(() => this.eventService.getSortedEventsByVotes());
  }

  sortByClosestDate(): void {
    this.eventCommunicationService.requestEventListRefresh(() => this.eventService.getSortedEventsByClosestDate());
  }
}
