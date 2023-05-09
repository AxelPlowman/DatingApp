import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [ // these children (below) will be protected by the guards in the canActivate array (above)
      { path: "members", component: MemberListComponent },
      { path: "members/:id", component: MemberDetailComponent },
      { path: "lists", component: ListsComponent },
      { path: "messages", component: MessagesComponent },
    ]
  },
  { path: "**", component: HomeComponent, pathMatch: 'full' } // anything that is not in the routes
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
