
import { Component, signal, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './services/api';
import { Question } from './models/question';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('Frontend');
  questions : Question[] = [];
  constructor(private apiService: ApiService) {}
  ngOnInit(){
    this.apiService.getQuestions().subscribe({
      next: (data) => {
        this.questions = data;
      },
      error: (err) => {
        console.error('Uh oh, something went wrong:', err);
      }
    });
  }
}
