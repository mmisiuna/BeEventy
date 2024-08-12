import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = 'http://localhost:5260/api/event';
  private ticketUrl = 'http://localhost:5260/api/ticket';

  constructor(private http: HttpClient) { }

  getAllEvents(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/getAllValidEvents`);
  }

  getLowestTicketPrice(eventId: number): Observable<number> {
    return this.http.get<number>(`${this.ticketUrl}/lowest-price/event/${eventId}`);
  }

  getSortedEventsByDate(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sort/date`);
  }

  getSortedEventsByVotes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sort/votes`);
  }

  getSortedEventsByClosestDate(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sort/closest`);
  }

  searchEvents(searchTerm: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search`, { params: { searchTerm } });
  }

  getEventById(eventId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${eventId}`);
  }
  
  addPlus(eventId: number, loginResponse: { token: string, userId: number }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${eventId}/plus`, loginResponse).pipe(
        tap(response => {
            console.log('Response from server:', response);
        })
    );
}

addMinus(eventId: number, loginResponse: { token: string, userId: number }): Observable<any> {
  return this.http.post<any>(`${this.apiUrl}/${eventId}/minus`, loginResponse).pipe(
    tap(response => {
      console.log('Response from server:', response);
    })
  );
}

  refreshEvent(eventId: number): Observable<any> {
    console.log('refreshEvent called with:', eventId);
    return this.http.get<any>(`${this.apiUrl}/${eventId}`);
  }
}
