import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
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
    private snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.intitializeForm();
  }

  intitializeForm() {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      knownAs: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
      password: [
        '',
        [Validators.required, Validators.maxLength(8), Validators.minLength(4)],
      ],
      confirmPassword: ['', [Validators.required, this.matchValue('password')]],
    });
  }

  matchValue(matchTo: string): ValidatorFn {
    return (control: any) => {
      return control?.value === control.parent?.controls[matchTo].value
        ? null
        : { isMatching: true };
    };
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
