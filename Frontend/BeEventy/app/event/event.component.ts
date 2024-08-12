import { Component, Input, OnInit } from '@angular/core';
import { EventService } from '../services/event.service';
import { UserService } from '../services/user.service';
import { Observable } from 'rxjs';
import { EventCommunicationService } from '../services/event-communication.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  @Input() event: any;
  lowestTicketPrice: number | undefined;
  emailOfLoggedUser$: Observable<string | null>;

  constructor(
    private eventService: EventService,
    private userService: UserService,
    private eventCommunicationService: EventCommunicationService
  ) { 
    this.emailOfLoggedUser$ = this.userService.getCurrentUserEmail();
  }

  ngOnInit(): void {
    this.eventService.getLowestTicketPrice(this.event.id).subscribe(
      price => {
        this.lowestTicketPrice = price;
      },
      error => {
        console.error('Error fetching lowest ticket price:', error);
        this.lowestTicketPrice = undefined; // Set to undefined to show 'N/A' in the UI
      }
    );
  }

  showDetails(): void {
    // Implement details display logic
  }
  
  voteDown(): void {
    this.emailOfLoggedUser$.subscribe(email => {
      if (email) {
        this.userService.getAccountByEmail(email).subscribe(
          user => {
            const token = localStorage.getItem('token');
            const loginResponse = { token: token || '', userId: user.id };
            this.eventService.addMinus(this.event.id, loginResponse).subscribe(
              updatedEvent => {
                console.log('Minus added and event updated:', updatedEvent);
                this.event = updatedEvent;
                this.eventCommunicationService.updateEventInList(updatedEvent);
              },
              error => {
                console.error('Error adding minus:', error);
              }
            );
          },
          error => {
            console.error('Error fetching user account by email:', error);
          }
        );
      } else {
        console.error('No user is logged in.');
      }
    });
  }
  
  

  voteUp(): void {
    this.emailOfLoggedUser$.subscribe(email => {
        if (email) {
            this.userService.getAccountByEmail(email).subscribe(
                user => {
                    const token = localStorage.getItem('token');
                    const loginResponse = { token: token || '', userId: user.id };
                    this.eventService.addPlus(this.event.id, loginResponse).subscribe(
                        updatedEvent => {
                            console.log('Plus added and event updated:', updatedEvent);
                            this.event = updatedEvent;
                            this.eventCommunicationService.updateEventInList(updatedEvent);
                        },
                        error => {
                            console.error('Error adding plus:', error);
                        }
                    );
                },
                error => {
                    console.error('Error fetching user account by email:', error);
                }
            );
        } else {
            console.error('No user is logged in.');
        }
    });
}

  
  refreshEvent(): void {
    this.eventService.refreshEvent(this.event.id).subscribe(
      updatedEvent => {
        console.log('Event refreshed:', updatedEvent);
        this.event = updatedEvent;
        this.eventCommunicationService.updateEventInList(updatedEvent);
      },
      error => {
        console.error('Error refreshing event:', error);
      }
    );
  }
  

  getLocationImage(location: number): string {
    switch (location) {
      case 0:
        return 'assets/reallife.png';
      case 1:
        return 'assets/global.png';
      case 2:
        return 'assets/Hybrid.png';
      default:
        return 'assets/reallife.PNG';
    }
  }

  getLocationText(location: number): string {
    switch (location) {
      case 0:
        return 'Na miejscu';
      case 1:
        return 'Online';
      case 2:
        return 'Hybrydowo';
      default:
        return '';
    }
  }

  getImageStyle(event: any): any {
    const aspectRatio = event.image.width / event.image.height;
    if (aspectRatio > 1) {
      return 'landscape-image';
    } else if (aspectRatio < 1) {
      return 'portrait-image';
    } else {
      return 'square-image';
    }
  }


  getLocationImageStyle(location: number): any {
    if (location === 0) {
      return { width: '2vw' };
    } else {
      return { width: '3vw' };
    }
  }

  loadImageError(event: Event): void {
    const target = event.target as HTMLImageElement;
    target.src = 'assets/events/default.PNG';
  }

  reportThisEvent(): void {
    console.log("eo");
  }
}
