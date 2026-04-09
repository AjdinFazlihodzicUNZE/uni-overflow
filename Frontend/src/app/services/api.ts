import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Answer } from '../models/answer';
import { Question } from '../models/question';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'http://localhost:5023/api';
  constructor(private http: HttpClient) {}

  getQuestions() {
    return this.http.get<Question[]>(`${this.baseUrl}/questions`);
  }
  postQuestion(question: Question) {
    return this.http.post<Question>(`${this.baseUrl}/questions`, question);
  } 
}
