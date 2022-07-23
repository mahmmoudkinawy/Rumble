import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss'],
})
export class ServerErrorComponent implements OnInit {
  reportedError: boolean = false;
  errorPercentage: number = 0;
  timer: any;

  constructor(private router: Router, private snackBar: MatSnackBar) {}

  ngOnInit() {}

  checkChanged = (event: any) => {
    this.reportedError = event.checked;
    this.reportedError ? this.startTimer() : this.stopTimer();
  };

  private startTimer = () => {
    this.timer = setInterval(() => {
      this.errorPercentage += 1;
      if (this.errorPercentage === 100) {
        this.router.navigateByUrl('/');
        this.snackBar.open('Thank you for your report', 'Close');
        clearInterval(this.timer);
      }
    }, 30);
  };

  private stopTimer = () => {
    clearInterval(this.timer);
    this.errorPercentage = 0;
  };
}
