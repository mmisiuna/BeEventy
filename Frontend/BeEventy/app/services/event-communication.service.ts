import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { UserService } from './user.service';
import { EventService } from './event.service';

@Injectable({
  providedIn: 'root'
})
export class EventCommunicationService {
  private refreshEventListSource = new Subject<() => Observable<any[]>>();
  refreshEventList$ = this.refreshEventListSource.asObservable();
  showNavbar = new BehaviorSubject<boolean | null>(null);
  currentUserEmail$ = this.showNavbar.asObservable(); 
  private updateEventInListSource = new Subject<any>();
  updateEventInList$ = this.updateEventInListSource.asObservable();
  constructor(
    private router: Router,
    private userService: UserService,
    private eventService: EventService) {
    }
  requestEventListRefresh(sortMethod: () => Observable<any[]>): void {
    this.refreshEventListSource.next(sortMethod);
  }

  navigateTo(path: string) {
    this.showNavBar();
    this.router.navigate([path]);
  }


  hideNavBar() {
    const navBar = document.getElementById('navigation-bar');
    if (navBar) {
      navBar.style.display = 'none';
    }
  }

  showNavBar() {
    const navBar = document.getElementById('navigation-bar');
    if (navBar) {
      navBar.style.display = 'block';
    }
  }
  updateEventInList(event: any): void {
    console.log('updateEventInList called with:', event);
    this.updateEventInListSource.next(event);
  }
  
  
}

