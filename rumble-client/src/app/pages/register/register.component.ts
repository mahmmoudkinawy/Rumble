import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: any = {};
  registerForm: FormGroup | null = null;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.intitializeForm();
  }

  intitializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
        Validators.maxLength(8),
        Validators.minLength(4),
      ]),
      confirmPassword: new FormControl('', [Validators.required]),
    });
  }

  register() {
    console.log(this.registerForm?.value);
    // this.accountService.register(this.model).subscribe(
    //   () => {
    //     this.router.navigateByUrl('/members');
    //   },
    //   (error) => {
    //     this.snackBar.open(error.error, 'Error');
    //   }
    // );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
