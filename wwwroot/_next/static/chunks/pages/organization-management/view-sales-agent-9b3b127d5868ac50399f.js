(self.webpackChunk_N_E=self.webpackChunk_N_E||[]).push([[301],{86520:function(e,t,r){"use strict";r.d(t,{Z:function(){return p}});var s=r(70885),n=r(67294),a=(r(64593),r(74096),r(59456),r(76914)),i=r(3838),l=r(88979),o=r(77750),c=r(56408),d=r(37645),u=r(85893);function p(e){var t=n.useState(!1),r=(0,s.Z)(t,2),p=(r[0],r[1]);return(0,u.jsxs)(i.Z,{open:!0,onClose:function(){p(!1)},"aria-labelledby":"alert-dialog-title","aria-describedby":"alert-dialog-description",children:[(0,u.jsx)(d.Z,{id:"alert-dialog-title",children:"Confirm"}),(0,u.jsx)(o.Z,{children:(0,u.jsx)(c.Z,{id:"alert-dialog-description",children:e.message})}),(0,u.jsxs)(l.Z,{children:[(0,u.jsx)(a.Z,{onClick:e.onConfirm,children:"Yes"}),(0,u.jsx)(a.Z,{onClick:e.onCancel,autoFocus:!0,children:"No"})]})]})}},30912:function(e,t,r){"use strict";var s=r(13264),n=r(62225),a=(0,s.Z)(n.$y)((function(){return{"& fieldset":{borderRadius:"0px",borderWidth:"1px"},"&.MuiFormControl-root":{width:"100%",height:"40px",padding:"0px",margin:"0px"}}}));t.Z=a},34678:function(e,t,r){"use strict";r.d(t,{x:function(){return u}});var s=r(4942),n=r(45987),a=(r(67294),r(2658)),i=r(71508),l=(r(74096),r(59456),r(85893)),o=["children","value","index"];function c(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(e);t&&(s=s.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,s)}return r}function d(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?c(Object(r),!0).forEach((function(t){(0,s.Z)(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):c(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function u(e){var t=e.children,r=e.value,s=e.index,c=(0,n.Z)(e,o);return(0,l.jsx)(a.Z,d(d({component:"div",role:"tabpanel",hidden:r!==s,id:"simple-tabpanel-".concat(s),"aria-labelledby":"simple-tab-".concat(s)},c),{},{children:r===s&&(0,l.jsx)(i.Z,{p:3,children:t})}))}},37949:function(e,t,r){"use strict";r.r(t),r.d(t,{default:function(){return J}});var s=r(4942),n=r(15671),a=r(43144),i=r(60136),l=r(6215),o=r(61120),c=r(67294),d=r(46458),u=r(11163),p=r(97554),h=r(98029),m=r(3945),f=r(98347),v=r(66543),x=r(80260),g=r(74338),j=r(74096),b=r(15861),N=r(87757),y=r.n(N),S=r(62225),C=r(32289),E=r(86520),A=r(30912),P=r(36501),Z=r(44656),w=r(62640),_=r(85166),O=r(59456),I=r(21275),D=r(90044),U=r(34051),R=r(31555),L=r(76914),k=r(34678),M=r(49645),T=r(85893);function z(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(e);t&&(s=s.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,s)}return r}function F(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?z(Object(r),!0).forEach((function(t){(0,s.Z)(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):z(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function Q(e){var t=function(){if("undefined"===typeof Reflect||!Reflect.construct)return!1;if(Reflect.construct.sham)return!1;if("function"===typeof Proxy)return!0;try{return Boolean.prototype.valueOf.call(Reflect.construct(Boolean,[],(function(){}))),!0}catch(e){return!1}}();return function(){var r,s=(0,o.Z)(e);if(t){var n=(0,o.Z)(this).constructor;r=Reflect.construct(s,arguments,n)}else r=s.apply(this,arguments);return(0,l.Z)(this,r)}}function B(e){return{id:"simple-tab-".concat(e),"aria-controls":"simple-tabpanel-".concat(e)}}var G=function(e){(0,i.Z)(r,e);var t=Q(r);function r(e){return(0,n.Z)(this,r),t.call(this,e)}return(0,a.Z)(r,[{key:"componentDidMount",value:function(){}},{key:"deleteItem",value:function(){var e=(0,b.Z)(y().mark((function e(t){var r;return y().wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return r={render:function(e,t,r){return(0,T.jsx)(E.Z,{message:e,onConfirm:t,onCancel:r})}},e.next=3,(0,M.i)("Are you sure you want to delete this Sales Agent?",r);case 3:if(!e.sent){e.next=7;break}return this.props.deleteItem(t,this.props.user),e.abrupt("return");case 7:case"end":return e.stop()}}),e,this)})));return function(t){return e.apply(this,arguments)}}()},{key:"addUser",value:function(){this.props.registerFromLocation(j.WR),this.props.addUser(this.props.user,this.props.lists,this.props.currentSalesAgent),this.props.router.push(j.Jx)}},{key:"viewUser",value:function(e){this.props.registerFromLocation(j.WR),this.props.viewUser(e,this.props.user,this.props.lists),this.props.router.push(j.ig)}},{key:"render",value:function(){var e=this;console.log("Get Edit ISO",this.props);var t=O.Fv(this.props.messages),r=[],s=[];this.props.lists&&this.props.lists.STATUS_LIST&&(this.props.lists.STATUS_LIST&&(s=this.props.lists.STATUS_LIST.map((function(e,t){return(0,T.jsx)(I.Z,{value:e.code,children:e.value},t)}))),this.props.lists.STATES&&(r=this.props.lists.STATES.map((function(e,t){return(0,T.jsx)(I.Z,{value:e.code,children:e.value},t)}))));var n=[];this.props.currentSalesAgent&&this.props.currentSalesAgent.Users&&this.props.currentSalesAgent.Users.length>0&&(n=this.props.currentSalesAgent.Users.filter((function(t){return(0==e.props.userSearchParams.emailId.toLowerCase().length||e.props.userSearchParams.emailId.toLowerCase().length>0&&t.Email_ID&&t.Email_ID.toLowerCase().includes(e.props.userSearchParams.emailId.toLowerCase()))&&(0==e.props.userSearchParams.firstName.toLowerCase().length||e.props.userSearchParams.firstName.toLowerCase().length>0&&t.First_Name&&t.First_Name.toLowerCase().includes(e.props.userSearchParams.firstName.toLowerCase()))&&(0==e.props.userSearchParams.lastName.toLowerCase().length||e.props.userSearchParams.lastName.toLowerCase().length>0&&t.Last_Name&&t.Last_Name.toLowerCase().includes(e.props.userSearchParams.lastName.toLowerCase()))&&(0==e.props.userSearchParams.city.toLowerCase().length||e.props.userSearchParams.city.toLowerCase().length>0&&t.City&&t.City.toLowerCase().includes(e.props.userSearchParams.city.toLowerCase()))})));var a=this.props.user.UserDetail?this.props.user.UserDetail.User_Level_Code:"",i=this.props.user.UserDetail&&"SALES-AGENT"==this.props.user.UserDetail.User_Level_Code;return(0,T.jsx)("div",{children:(0,T.jsx)("div",{className:"row",children:(0,T.jsx)("div",{className:"col-lg-12",children:(0,T.jsx)("div",{className:"card",children:(0,T.jsx)("div",{className:"card-body",children:(0,T.jsx)("div",{className:"row",children:(0,T.jsxs)("div",{className:"col-12",id:"search",children:[(0,T.jsxs)("div",{className:"row",children:[(0,T.jsx)("div",{className:"col-lg-6 col-md-6",children:(0,T.jsx)("h4",{children:"Sales Agent "})}),(0,T.jsx)("div",{className:"col-lg-6 col-md-6 text-right"})]}),(0,T.jsxs)(S.WS,{onSubmit:function(t){return e.props.processForm(e.props.currentSalesAgent,e.props.user,e.props.lists)},children:[t&&t.length>0&&(0,T.jsx)("div",{id:"error",className:"btn-outline-danger",children:t}),(0,T.jsxs)(P.Z,{children:[(0,T.jsxs)(Z.Z,{value:this.props.panel,onChange:this.props.handlePanelChange,"aria-label":"simple tabs example",indicatorColor:"primary",variant:"scrollable",scrollButtons:"auto",textColor:"primary",children:[(0,T.jsx)(w.Z,F(F({label:"Sales Agent Information"},B(0)),{},{disableRipple:!0})),(0,T.jsx)(w.Z,F(F({label:"Users "},B(0)),{},{disableRipple:!0}))]}),(0,T.jsxs)(k.x,{value:this.props.panel,index:0,children:[(0,T.jsxs)("div",{className:"row",children:[(0,T.jsx)("div",{className:"col-md-12",children:(0,T.jsx)("h4",{className:"card-description",children:"General Information"})}),(0,T.jsxs)("div",{className:"col-xl-4 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"First Name:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.First_Name),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"First_Name",id:"First_Name",placeholder:"First Name  ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-4 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Middle Name:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Middle_Name),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Middle_Name",id:"Middle_Name",placeholder:"Middle Name  ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-4 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Last Name:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Last_Name),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Last_Name",id:"Last_Name",placeholder:"Last Name  ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-4 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Active Status:"}),(0,T.jsx)("br",{}),(0,T.jsx)(A.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Active_status_code),onChange:this.props.handleEditItemChange,validators:["required"],variant:"outlined",size:"small",errorMessages:["Required"],disabled:i,inputProps:{name:"Active_status_code",id:"Active_status_code"},children:s})]}),(0,T.jsxs)("div",{className:"col-xl-4 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Code:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Code),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",disabled:i,inputProps:{name:"Code",id:"Code",placeholder:"Sales Agent Code  ...",type:"text"}})]}),(0,T.jsx)("div",{className:"col-md-12 pt-3",children:(0,T.jsx)("h4",{className:"card-description",children:"Mailing Address"})}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"Street Address:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Street_Address),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Street_Address",id:"Street_Address",placeholder:"Street Address  ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"City:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.City),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"City",id:"City",placeholder:"City  ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"State:"}),(0,T.jsx)("br",{}),(0,T.jsx)(A.Z,{value:O.NA(this.props.currentSalesAgent.Detail.State),onChange:this.props.handleEditItemChange,validators:["required"],variant:"outlined",size:"small",errorMessages:["Required"],inputProps:{name:"State",id:"State"},children:r})]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"Zip:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.ZIP),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"ZIP",id:"ZIP",placeholder:"Zip  ...",type:"ZIP"}})]}),(0,T.jsx)("div",{className:"col-md-12 pt-3",children:(0,T.jsx)("h4",{className:"card-description",children:"Contact "})}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"Email:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Email_ID),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Email_ID",id:"Email_ID",placeholder:"Email   ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"Main Phone:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Main_Phone),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Main_Phone",id:"Main_Phone",placeholder:"Main Phone   ...",type:"text"}})]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-3 col-md-4",children:[(0,T.jsx)("b",{children:"Cell Phone:"}),(0,T.jsx)("br",{}),(0,T.jsx)(C.Z,{value:O.NA(this.props.currentSalesAgent.Detail.Cell_Phone),onChange:this.props.handleEditItemChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"Cell_Phone",id:"Cell_Phone",placeholder:"Cell Phone   ...",type:"text"}})]}),(0,T.jsx)("div",{className:"col-md-12 pt-3",children:(0,T.jsx)("h4",{className:"card-description",children:"Update Information"})}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Date Added:"})," ",O.NA(this.props.currentSalesAgent.Detail.DateTime_Added)]}),(0,T.jsxs)("div",{className:"col-xl-3 col-lg-4 col-md-4",children:[(0,T.jsx)("b",{children:"Date Updated:"})," ",O.NA(this.props.currentSalesAgent.Detail.DateTime_Updated)]})]}),(0,T.jsx)(U.Z,{children:(0,T.jsx)(R.Z,{md:"12",children:"\xa0"})}),(0,T.jsxs)(U.Z,{children:[(0,T.jsx)(R.Z,{md:"3",children:(0,T.jsx)(L.Z,{variant:"contained",type:"button",onClick:function(){return e.props.router.back()},className:"col-md-12",children:"Back"})}),(0,T.jsx)(R.Z,{md:"3",children:this.props.currentSalesAgent.Detail&&this.props.currentSalesAgent.Detail.AutoIdent&&"SALES-AGENT"!=a&&(0,T.jsx)(L.Z,{variant:"contained",color:"error",type:"button",onClick:function(){return e.deleteItem(e.props.currentSalesAgent)},className:"col-md-12",children:"Delete"})}),(0,T.jsx)(R.Z,{md:"3",children:"\xa0"}),(0,T.jsx)(R.Z,{md:"3",children:(0,T.jsx)(L.Z,{variant:"contained",color:"success",type:"submit",className:"col-md-12",children:" Save "})})]})]}),(0,T.jsx)(k.x,{value:this.props.panel,index:1,children:(0,T.jsx)("div",{className:"row",children:(0,T.jsxs)("div",{className:"col-12",id:"search",children:[(0,T.jsxs)(U.Z,{children:[(0,T.jsxs)(R.Z,{md:3,children:[(0,T.jsx)("div",{className:"pb-10",children:"Email "}),(0,T.jsx)(C.Z,{value:O.NA(this.props.userSearchParams.emailId),onChange:this.props.handleUserSearchChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"emailId",id:"emailId",placeholder:"Email  ...",type:"text"}})]}),(0,T.jsxs)(R.Z,{md:3,children:[(0,T.jsx)("div",{className:"pb-10",children:"First Name "}),(0,T.jsx)(C.Z,{value:O.NA(this.props.userSearchParams.firstName),onChange:this.props.handleUserSearchChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"firstName",id:"firstName",placeholder:"First Name  ...",type:"text"}})]}),(0,T.jsxs)(R.Z,{md:3,children:[(0,T.jsx)("div",{className:"pb-10",children:"Last Name "}),(0,T.jsx)(C.Z,{value:O.NA(this.props.userSearchParams.lastName),onChange:this.props.handleUserSearchChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"lastName",id:"lastName",placeholder:"Last Name  ...",type:"text"}})]}),(0,T.jsxs)(R.Z,{md:2,children:[(0,T.jsx)("div",{className:"pb-10",children:"City "}),(0,T.jsx)(C.Z,{value:O.NA(this.props.userSearchParams.city),onChange:this.props.handleUserSearchChange,validators:[],className:"col-md-12",m:20,errorMessages:["REQUIRED"],variant:"outlined",size:"small",inputProps:{name:"city",id:"city",placeholder:"City  ...",type:"text"}})]}),(0,T.jsxs)(R.Z,{md:1,className:"text-bottom text-right",children:[(0,T.jsx)("div",{className:"pb-10",children:"\xa0 "}),(0,T.jsxs)(L.Z,{variant:"contained",color:"primary",type:"button",onClick:function(){return e.addUser()},className:"",children:[" ",(0,T.jsx)("i",{className:"ti-plus menu-icon"})," ",(0,T.jsx)("br",{})]})]})]}),(0,T.jsx)(D.ZP,{title:"Users",onRowClicked:function(t){return e.viewUser(t)},data:n,columns:[{selector:function(e){return e.Email_ID},name:"Email",sortable:!0},{selector:function(e){return e.First_Name},name:"First Name",sortable:!0},{selector:function(e){return e.Last_Name},name:"Last Name",sortable:!0},{selector:function(e){return e.City},name:"City",sortable:!0},{selector:function(e){return e.User_Level_Code},name:"User Access",sortable:!0}],pagination:!0})]})})})]})]})]})})})})})})})}}]),r}(c.Component);var q=(0,u.withRouter)((0,d.$j)((function(e){return{panel:e.salesAgentOrganizationManagement.panel}}),(function(e){return{dispatch:e,handlePanelChange:function(t,r){return e(g._J(r))}}}))(G)),W=r(87653);function Y(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(e);t&&(s=s.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),r.push.apply(r,s)}return r}function V(e){for(var t=1;t<arguments.length;t++){var r=null!=arguments[t]?arguments[t]:{};t%2?Y(Object(r),!0).forEach((function(t){(0,s.Z)(e,t,r[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):Y(Object(r)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(r,t))}))}return e}function $(e){var t=function(){if("undefined"===typeof Reflect||!Reflect.construct)return!1;if(Reflect.construct.sham)return!1;if("function"===typeof Proxy)return!0;try{return Boolean.prototype.valueOf.call(Reflect.construct(Boolean,[],(function(){}))),!0}catch(e){return!1}}();return function(){var r,s=(0,o.Z)(e);if(t){var n=(0,o.Z)(this).constructor;r=Reflect.construct(s,arguments,n)}else r=s.apply(this,arguments);return(0,l.Z)(this,r)}}var H=function(e){(0,i.Z)(r,e);var t=$(r);function r(e){return(0,n.Z)(this,r),t.call(this,e)}return(0,a.Z)(r,[{key:"componentDidMount",value:function(){console.log("View ISO",this.props.currentSalesAgent),this.props.currentSalesAgent&&this.props.currentSalesAgent.Detail&&this.props.currentSalesAgent.Detail.Code&&this.props.getItem(this.props.currentSalesAgent,this.props.user)}},{key:"render",value:function(){return console.log("ViewSalesAgent Index",this.props),(0,T.jsxs)("div",{className:"container-scroller",children:[(0,T.jsx)(h.Z,V({title:"Eagle Processing Sales Office",description:"Eagle Processing Sales Office"},this.props)),(0,T.jsx)(f.Z,V({},this.props)),(0,T.jsxs)("div",{className:"container-fluid page-body-wrapper",children:[(0,T.jsx)(v.Z,V({},this.props)),(0,T.jsxs)("div",{className:"main-panel",children:[(0,T.jsxs)("div",{className:"content-wrapper",children:[(0,T.jsx)(q,V({},this.props)),(0,T.jsx)(x.Z,V({},this.props))]}),(0,T.jsx)(p.Z,V({},this.props))]}),(0,T.jsx)(m.Z,V({},this.props))]})]})}}]),r}(c.PureComponent);var J=(0,u.withRouter)((0,d.$j)((function(e){return{task:e.merchantAddStep1.task,loading:e.merchantAddStep1.loading,messages:e.central.messages,moveToUrl:e.merchantAddStep1.moveToUrl,lists:e.central.lists,user:e.central.user,currentSalesAgent:e.salesAgentOrganizationManagement.currentItem,changeState:e.salesAgentOrganizationManagement.changeState,userSearchParams:e.salesAgentOrganizationManagement.userSearchParams}}),(function(e){return{dispatch:e,resetTask:function(){return e(g.resetTaskAction())},getItem:function(t,r){return e(g.rV(t,r))},navigateToUrl:function(t,r){return e(g.moveToUrlAction(t,r))},processForm:function(t,r){return e(g.OK(t,r))},handleItemChange:function(t,r){return e(g.YM(t.target.name,t.target.value,r))},handleUserSearchChange:function(t,r){return e(g.OT(t.target.name,t.target.value,r))},handleEditItemChange:function(t,r){return e(g.G1(t.target.name,t.target.value,r))},viewUser:function(t,r,s){return e(W.HY(t,r,s))},deleteItem:function(t,r,s){return e(g.wz(t,r,s))},addUser:function(t,r,s){return e(_.cn(t,r,s,"SALES-AGENT"))},registerFromLocation:function(t){return e(_.Qi(t))}}}))(H))},70185:function(e,t,r){(window.__NEXT_P=window.__NEXT_P||[]).push(["/organization-management/view-sales-agent",function(){return r(37949)}])},49645:function(e,t,r){function s(e){return e&&"object"==typeof e&&"default"in e?e.default:e}var n=r(73935),a=s(n),i=r(67294),l=s(i);var o="confirm-box-root_"+Math.random().toString(36).substr(2,9),c=function(e){e=e.children;var t=document.getElementById(o),r=document.createElement("div");return i.useEffect((function(){return t.appendChild(r),function(){return t.removeChild(r)}}),[r,t]),n.createPortal(e,r)};!function(e){if(e&&window){const t=document.createElement("style");t.setAttribute("type","text/css"),t.innerHTML=e,document.head.appendChild(t)}}(".confirm-box {\n  z-index: 1000;\n  position: absolute;\n  left: 45%;\n  top: 45%;\n}\n.confirm-box__content {\n  z-index: 300;\n  background-color: #fff;\n  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);\n  padding: 1em;\n  border-radius: 5px;\n  width: 300px;\n  max-width: 400px;\n}\n.confirm-box__overlay {\n  z-index: -1;\n  position: fixed;\n  top: 0;\n  left: 0;\n  width: 100vw;\n  height: 100vh;\n  background-color: rgba(0, 0, 0, 0.1);\n}\n.confirm-box__actions {\n  display: flex;\n  padding-top: 1em;\n  justify-content: flex-end;\n}\n.confirm-box__actions > :not(:last-child) {\n  margin-right: 1em;\n}");var d=function(e){function t(){p(!1),o(!0)}function r(){p(!1),o(!1)}var s,n,a,o=e.resolver,c=e.message,d=e.options,u=i.useState(!0),p=(e=u[0],u[1]);return e?l.createElement("div",{className:"confirm-box"},(n="confirm-box__content "+((null==(s=(d||{}).classNames)?void 0:s.container)||"")+"\n    ",a=((null==s?void 0:s.buttons)||"")+" "+((null==s?void 0:s.confirmButton)||"")+"\n    ",s=((null==s?void 0:s.buttons)||"")+" "+((null==s?void 0:s.cancelButton)||""),null!=d&&d.render?d.render(c,t,r):l.createElement("div",{className:n},l.createElement("span",null,c),l.createElement("div",{className:"confirm-box__actions"},l.createElement("button",{onClick:t,role:"confirmable-button",className:a},null!==(a=null==d?void 0:d.labels)&&void 0!==a&&a.confirmable?null===(a=null==d?void 0:d.labels)||void 0===a?void 0:a.confirmable:"Yes"),l.createElement("button",{onClick:r,role:"cancellable-button",className:s},null!==(s=null==d?void 0:d.labels)&&void 0!==s&&s.cancellable?null===(s=null==d?void 0:d.labels)||void 0===s?void 0:s.cancellable:"No")))),l.createElement("div",{className:"confirm-box__overlay",onClick:function(){null!=d&&d.closeOnOverlayClick&&(p(!1),o(!1))}})):null};t.i=function(e,t){return r=void 0,s=void 0,i=function(){var r;return function(e,t){var r,s,n,a={label:0,sent:function(){if(1&n[0])throw n[1];return n[1]},trys:[],ops:[]},i={next:l(0),throw:l(1),return:l(2)};return"function"==typeof Symbol&&(i[Symbol.iterator]=function(){return this}),i;function l(i){return function(l){return function(i){if(r)throw new TypeError("Generator is already executing.");for(;a;)try{if(r=1,s&&(n=2&i[0]?s.return:i[0]?s.throw||((n=s.return)&&n.call(s),0):s.next)&&!(n=n.call(s,i[1])).done)return n;switch(s=0,(i=n?[2&i[0],n.value]:i)[0]){case 0:case 1:n=i;break;case 4:return a.label++,{value:i[1],done:!1};case 5:a.label++,s=i[1],i=[0];continue;case 7:i=a.ops.pop(),a.trys.pop();continue;default:if(!(n=0<(n=a.trys).length&&n[n.length-1])&&(6===i[0]||2===i[0])){a=0;continue}if(3===i[0]&&(!n||i[1]>n[0]&&i[1]<n[3])){a.label=i[1];break}if(6===i[0]&&a.label<n[1]){a.label=n[1],n=i;break}if(n&&a.label<n[2]){a.label=n[2],a.ops.push(i);break}n[2]&&a.ops.pop(),a.trys.pop();continue}i=t.call(e,a)}catch(l){i=[6,l],s=0}finally{r=n=0}if(5&i[0])throw i[1];return{value:i[0]?i[1]:void 0,done:!0}}([i,l])}}}(this,(function(s){switch(s.label){case 0:return[4,document.getElementById(o)];case 1:return s.sent()?[3,4]:[4,document.createElement("div")];case 2:return[4,(r=s.sent()).setAttribute("id",o)];case 3:s.sent(),document.body.appendChild(r),s.label=4;case 4:return[2,new Promise((function(r){r=l.createElement(d,{resolver:r,message:e,options:t}),r=l.createElement(c,null,r),a.render(r,document.getElementById(o))}))]}}))},new(n=(n=void 0)||Promise)((function(e,t){function a(e){try{o(i.next(e))}catch(e){t(e)}}function l(e){try{o(i.throw(e))}catch(e){t(e)}}function o(t){t.done?e(t.value):new n((function(e){e(t.value)})).then(a,l)}o((i=i.apply(r,s||[])).next())}));var r,s,n,i}}},function(e){e.O(0,[3034,6541,7194,44,6914,421,7994,1157,5402,882,9774,2888,179],(function(){return t=70185,e(e.s=t);var t}));var t=e.O();_N_E=t}]);