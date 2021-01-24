import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service: SharedService) { }

  DepartmentList: any[] = [];

   ModalTitle:string;
  ActivateAddEditDepComp:boolean=false;
  dep:any;

  ngOnInit(): void {
    this.refreshDepList();
  }

  refreshDepList() {
    this.service.getDepList().subscribe(res => {
      console.log('Department List: ',res);
      this.DepartmentList = res;
    });
  }

  addClick() {
    this.dep = {
      DepartmentId: 0,
      DepartmentName: ''
    };
    this.ModalTitle = "Add Department";
    this.ActivateAddEditDepComp = true;
  }

  editClick(data: any) {
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
    this.dep = data;
  }

  deleteClick(data: any) {
    if (confirm('Are you sure you want to delete?')) {
      this.service.deleteDepartment(data.DepartmentId).subscribe(res => {
        alert(res.toString());
        this.refreshDepList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }
}
