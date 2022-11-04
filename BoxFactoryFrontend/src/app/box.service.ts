import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {catchError, Observable, of, tap} from "rxjs";
import {Box} from "./box.entity";

@Injectable({
  providedIn: 'root'
})
export class BoxService {
  private boxURL = "https://localhost:7023/BoxApi/";

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  constructor(private http: HttpClient) {
  }

  readAll(): Observable<Box[]> {
    return this.http.get<Box[]>(this.boxURL)
      .pipe(
        tap(_ => this.log('fetch all boxes')),
        catchError(this.handleError<Box[]>('readAll', []))
      );
  }

  createBox(dto: { width: number; height: number }): Observable<Box> {
    return this.http.post<Box>(this.boxURL, dto, this.httpOptions)
      .pipe(
        tap((createdBox: Box) => this.log('created new box with id: ' + createdBox.id)),
        catchError(this.handleError<Box>('createBox'))
      );
  }

  deleteBox(id: number): Observable<Box> {
    return this.http.delete<Box>(this.boxURL + id, this.httpOptions)
      .pipe(
        tap(_ => this.log('deleted box with id: ' + id)),
        catchError(this.handleError<Box>('deleteBox'))
      )
  }

  updateBox(box: Box): Observable<any> {
    return this.http.put(this.boxURL + box.id, box, this.httpOptions)
      .pipe(
        tap(_ => this.log('updated box')),
        catchError(this.handleError<any>('updateBox'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(`ItemService: ${message}`);
    //alert(`ItemService: ${message}`);
  }
}
