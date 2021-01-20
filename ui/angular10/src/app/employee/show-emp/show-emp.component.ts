import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service'

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private shared: SharedService) { }

  EmployeeList: any[] = [];


  ngOnInit(): void {
    this.refreshEmpList();
  }

  refreshEmpList() {
    this.shared.getEmpList().subscribe(res => {
      console.log("Employee List: ", res);
      this.EmployeeList = res;
    });
  }

}
