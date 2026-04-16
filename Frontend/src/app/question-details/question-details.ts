import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ApiService } from '../services/api';
import { Question } from '../models/question';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-question-details',
  imports: [CommonModule, RouterModule],
  templateUrl: './question-details.html',
  styleUrl: './question-details.css',
  standalone: true
})
export class QuestionDetails implements OnInit {
  id: string | null = '';
  currentQuestion? : Question;
  constructor(private route: ActivatedRoute, private apiService: ApiService) {}
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if(this.id) {
        this.apiService.getQuestionById(this.id).subscribe(data => {
          this.currentQuestion = data;
          console.log("Fetched question details: ", this.currentQuestion);
        });
      } else {
        console.error("No ID found in route parameters.");
      }   
    });
  }
} 
