import { Component, signal, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { ApiService } from '../services/api';
import { Question } from '../models/question';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-home',
  imports: [RouterOutlet, RouterLink, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
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
  newQuestion: Question = {id:'', 
    title: '', 
    content: '',
     authorName: '', 
    isApproved: false,
     createdAt: '',
      answers: []
  };
  postQuestion() {
    this.apiService.postQuestion(this.newQuestion).subscribe({
      next: (data) => {
        console.log('Question posted successfully:', data);
        this.questions.push(data);
        this.newQuestion = {id:'', 
        title: '', 
        content: '',
          authorName: '',
          isApproved: false,
          createdAt: '',
          answers: []
          };
      },
      error: (err) => {
        console.error('Error posting question:', err);
      }
    });
  }
}
