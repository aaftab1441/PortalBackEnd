(self.webpackChunk_N_E=self.webpackChunk_N_E||[]).push([[3759],{18521:function(e,t,o){"use strict";o.d(t,{Z:function(){return ce}});var a=o(1048),r=o(32793),n=o(67294),l=o(86010),i=o(57579),s=o(8925),c=o(73633),p=o(7960);function d(e){return"undefined"!==typeof e.normalize?e.normalize("NFD").replace(/[\u0300-\u036f]/g,""):e}function u(e,t){for(let o=0;o<e.length;o+=1)if(t(e[o]))return o;return-1}const g=function(e={}){const{ignoreAccents:t=!0,ignoreCase:o=!0,limit:a,matchFrom:r="any",stringify:n,trim:l=!1}=e;return(e,{inputValue:i,getOptionLabel:s})=>{let c=l?i.trim():i;o&&(c=c.toLowerCase()),t&&(c=d(c));const p=e.filter((e=>{let a=(n||s)(e);return o&&(a=a.toLowerCase()),t&&(a=d(a)),"start"===r?0===a.indexOf(c):a.indexOf(c)>-1}));return"number"===typeof a?p.slice(0,a):p}}();function m(e){const{autoComplete:t=!1,autoHighlight:o=!1,autoSelect:a=!1,blurOnSelect:l=!1,disabled:d,clearOnBlur:m=!e.freeSolo,clearOnEscape:b=!1,componentName:f="useAutocomplete",defaultValue:h=(e.multiple?[]:null),disableClearable:v=!1,disableCloseOnSelect:y=!1,disabledItemsFocusable:x=!1,disableListWrap:Z=!1,filterOptions:$=g,filterSelectedOptions:C=!1,freeSolo:S=!1,getOptionDisabled:I,getOptionLabel:k=(e=>{var t;return null!=(t=e.label)?t:e}),isOptionEqualToValue:O=((e,t)=>e===t),groupBy:w,handleHomeEndKeys:P=!e.freeSolo,id:R,includeInputInList:L=!1,inputValue:z,multiple:M=!1,onChange:T,onClose:A,onHighlightChange:F,onInputChange:N,onOpen:D,open:V,openOnFocus:E=!1,options:j,readOnly:q=!1,selectOnFocus:H=!e.freeSolo,value:W}=e,B=(0,i.Z)(R);let G=k;G=e=>{const t=k(e);return"string"!==typeof t?String(t):t};const K=n.useRef(!1),U=n.useRef(!0),_=n.useRef(null),J=n.useRef(null),[Q,X]=n.useState(null),[Y,ee]=n.useState(-1),te=o?0:-1,oe=n.useRef(te),[ae,re]=(0,s.Z)({controlled:W,default:h,name:f}),[ne,le]=(0,s.Z)({controlled:z,default:"",name:f,state:"inputValue"}),[ie,se]=n.useState(!1),ce=n.useCallback(((e,t)=>{if(!(M?ae.length<t.length:null!==t)&&!m)return;let o;if(M)o="";else if(null==t)o="";else{const e=G(t);o="string"===typeof e?e:""}ne!==o&&(le(o),N&&N(e,o,"reset"))}),[G,ne,M,N,le,m,ae]),pe=n.useRef();n.useEffect((()=>{const e=ae!==pe.current;pe.current=ae,ie&&!e||S&&!e||ce(null,ae)}),[ae,ce,ie,pe,S]);const[de,ue]=(0,s.Z)({controlled:V,default:!1,name:f,state:"open"}),[ge,me]=n.useState(!0),be=!M&&null!=ae&&ne===G(ae),fe=de&&!q,he=fe?$(j.filter((e=>!C||!(M?ae:[ae]).some((t=>null!==t&&O(e,t))))),{inputValue:be&&ge?"":ne,getOptionLabel:G}):[],ve=de&&he.length>0&&!q;const ye=(0,c.Z)((e=>{-1===e?_.current.focus():Q.querySelector(`[data-tag-index="${e}"]`).focus()}));n.useEffect((()=>{M&&Y>ae.length-1&&(ee(-1),ye(-1))}),[ae,M,Y,ye]);const xe=(0,c.Z)((({event:e,index:t,reason:o="auto"})=>{if(oe.current=t,-1===t?_.current.removeAttribute("aria-activedescendant"):_.current.setAttribute("aria-activedescendant",`${B}-option-${t}`),F&&F(e,-1===t?null:he[t],o),!J.current)return;const a=J.current.querySelector('[role="option"].Mui-focused');a&&(a.classList.remove("Mui-focused"),a.classList.remove("Mui-focusVisible"));const r=J.current.parentElement.querySelector('[role="listbox"]');if(!r)return;if(-1===t)return void(r.scrollTop=0);const n=J.current.querySelector(`[data-option-index="${t}"]`);if(n&&(n.classList.add("Mui-focused"),"keyboard"===o&&n.classList.add("Mui-focusVisible"),r.scrollHeight>r.clientHeight&&"mouse"!==o)){const e=n,t=r.clientHeight+r.scrollTop,o=e.offsetTop+e.offsetHeight;o>t?r.scrollTop=o-r.clientHeight:e.offsetTop-e.offsetHeight*(w?1.3:0)<r.scrollTop&&(r.scrollTop=e.offsetTop-e.offsetHeight*(w?1.3:0))}})),Ze=(0,c.Z)((({event:e,diff:o,direction:a="next",reason:r="auto"})=>{if(!fe)return;const n=function(e,t){if(!J.current||-1===e)return-1;let o=e;for(;;){if("next"===t&&o===he.length||"previous"===t&&-1===o)return-1;const e=J.current.querySelector(`[data-option-index="${o}"]`),a=!x&&(!e||e.disabled||"true"===e.getAttribute("aria-disabled"));if(!(e&&!e.hasAttribute("tabindex")||a))return o;o+="next"===t?1:-1}}((()=>{const e=he.length-1;if("reset"===o)return te;if("start"===o)return 0;if("end"===o)return e;const t=oe.current+o;return t<0?-1===t&&L?-1:Z&&-1!==oe.current||Math.abs(o)>1?0:e:t>e?t===e+1&&L?-1:Z||Math.abs(o)>1?e:0:t})(),a);if(xe({index:n,reason:r,event:e}),t&&"reset"!==o)if(-1===n)_.current.value=ne;else{const e=G(he[n]);_.current.value=e;0===e.toLowerCase().indexOf(ne.toLowerCase())&&ne.length>0&&_.current.setSelectionRange(ne.length,e.length)}})),$e=n.useCallback((()=>{if(!fe)return;const e=M?ae[0]:ae;if(0!==he.length&&null!=e){if(J.current)if(null==e)oe.current>=he.length-1?xe({index:he.length-1}):xe({index:oe.current});else{const t=he[oe.current];if(M&&t&&-1!==u(ae,(e=>O(t,e))))return;const o=u(he,(t=>O(t,e)));-1===o?Ze({diff:"reset"}):xe({index:o})}}else Ze({diff:"reset"})}),[he.length,!M&&ae,C,Ze,xe,fe,ne,M]),Ce=(0,c.Z)((e=>{(0,p.Z)(J,e),e&&$e()}));n.useEffect((()=>{$e()}),[$e]);const Se=e=>{de||(ue(!0),me(!0),D&&D(e))},Ie=(e,t)=>{de&&(ue(!1),A&&A(e,t))},ke=(e,t,o,a)=>{if(M){if(ae.length===t.length&&ae.every(((e,o)=>e===t[o])))return}else if(ae===t)return;T&&T(e,t,o,a),re(t)},Oe=n.useRef(!1),we=(e,t,o="selectOption",a="options")=>{let r=o,n=t;if(M){n=Array.isArray(ae)?ae.slice():[];const e=u(n,(e=>O(t,e)));-1===e?n.push(t):"freeSolo"!==a&&(n.splice(e,1),r="removeOption")}ce(e,n),ke(e,n,r,{option:t}),y||e.ctrlKey||e.metaKey||Ie(e,r),(!0===l||"touch"===l&&Oe.current||"mouse"===l&&!Oe.current)&&_.current.blur()};const Pe=(e,t)=>{if(!M)return;""===ne&&Ie(e,"toggleInput");let o=Y;-1===Y?""===ne&&"previous"===t&&(o=ae.length-1):(o+="next"===t?1:-1,o<0&&(o=0),o===ae.length&&(o=-1)),o=function(e,t){if(-1===e)return-1;let o=e;for(;;){if("next"===t&&o===ae.length||"previous"===t&&-1===o)return-1;const e=Q.querySelector(`[data-tag-index="${o}"]`);if(e&&e.hasAttribute("tabindex")&&!e.disabled&&"true"!==e.getAttribute("aria-disabled"))return o;o+="next"===t?1:-1}}(o,t),ee(o),ye(o)},Re=e=>{K.current=!0,le(""),N&&N(e,"","clear"),ke(e,M?[]:null,"clear")},Le=e=>o=>{if(e.onKeyDown&&e.onKeyDown(o),!o.defaultMuiPrevented&&(-1!==Y&&-1===["ArrowLeft","ArrowRight"].indexOf(o.key)&&(ee(-1),ye(-1)),229!==o.which))switch(o.key){case"Home":fe&&P&&(o.preventDefault(),Ze({diff:"start",direction:"next",reason:"keyboard",event:o}));break;case"End":fe&&P&&(o.preventDefault(),Ze({diff:"end",direction:"previous",reason:"keyboard",event:o}));break;case"PageUp":o.preventDefault(),Ze({diff:-5,direction:"previous",reason:"keyboard",event:o}),Se(o);break;case"PageDown":o.preventDefault(),Ze({diff:5,direction:"next",reason:"keyboard",event:o}),Se(o);break;case"ArrowDown":o.preventDefault(),Ze({diff:1,direction:"next",reason:"keyboard",event:o}),Se(o);break;case"ArrowUp":o.preventDefault(),Ze({diff:-1,direction:"previous",reason:"keyboard",event:o}),Se(o);break;case"ArrowLeft":Pe(o,"previous");break;case"ArrowRight":Pe(o,"next");break;case"Enter":if(-1!==oe.current&&fe){const e=he[oe.current],a=!!I&&I(e);if(o.preventDefault(),a)return;we(o,e,"selectOption"),t&&_.current.setSelectionRange(_.current.value.length,_.current.value.length)}else S&&""!==ne&&!1===be&&(M&&o.preventDefault(),we(o,ne,"createOption","freeSolo"));break;case"Escape":fe?(o.preventDefault(),o.stopPropagation(),Ie(o,"escape")):b&&(""!==ne||M&&ae.length>0)&&(o.preventDefault(),o.stopPropagation(),Re(o));break;case"Backspace":if(M&&!q&&""===ne&&ae.length>0){const e=-1===Y?ae.length-1:Y,t=ae.slice();t.splice(e,1),ke(o,t,"removeOption",{option:ae[e]})}}},ze=e=>{se(!0),E&&!K.current&&Se(e)},Me=e=>{null!==J.current&&J.current.parentElement.contains(document.activeElement)?_.current.focus():(se(!1),U.current=!0,K.current=!1,a&&-1!==oe.current&&fe?we(e,he[oe.current],"blur"):a&&S&&""!==ne?we(e,ne,"blur","freeSolo"):m&&ce(e,ae),Ie(e,"blur"))},Te=e=>{const t=e.target.value;ne!==t&&(le(t),me(!1),N&&N(e,t,"input")),""===t?v||M||ke(e,null,"clear"):Se(e)},Ae=e=>{xe({event:e,index:Number(e.currentTarget.getAttribute("data-option-index")),reason:"mouse"})},Fe=()=>{Oe.current=!0},Ne=e=>{const t=Number(e.currentTarget.getAttribute("data-option-index"));we(e,he[t],"selectOption"),Oe.current=!1},De=e=>t=>{const o=ae.slice();o.splice(e,1),ke(t,o,"removeOption",{option:ae[e]})},Ve=e=>{de?Ie(e,"toggleInput"):Se(e)},Ee=e=>{e.target.getAttribute("id")!==B&&e.preventDefault()},je=()=>{_.current.focus(),H&&U.current&&_.current.selectionEnd-_.current.selectionStart===0&&_.current.select(),U.current=!1},qe=e=>{""!==ne&&de||Ve(e)};let He=S&&ne.length>0;He=He||(M?ae.length>0:null!==ae);let We=he;if(w){new Map;We=he.reduce(((e,t,o)=>{const a=w(t);return e.length>0&&e[e.length-1].group===a?e[e.length-1].options.push(t):e.push({key:o,index:o,group:a,options:[t]}),e}),[])}return d&&ie&&Me(),{getRootProps:(e={})=>(0,r.Z)({"aria-owns":ve?`${B}-listbox`:null},e,{onKeyDown:Le(e),onMouseDown:Ee,onClick:je}),getInputLabelProps:()=>({id:`${B}-label`,htmlFor:B}),getInputProps:()=>({id:B,value:ne,onBlur:Me,onFocus:ze,onChange:Te,onMouseDown:qe,"aria-activedescendant":fe?"":null,"aria-autocomplete":t?"both":"list","aria-controls":ve?`${B}-listbox`:void 0,"aria-expanded":ve,autoComplete:"off",ref:_,autoCapitalize:"none",spellCheck:"false",role:"combobox"}),getClearProps:()=>({tabIndex:-1,onClick:Re}),getPopupIndicatorProps:()=>({tabIndex:-1,onClick:Ve}),getTagProps:({index:e})=>(0,r.Z)({key:e,"data-tag-index":e,tabIndex:-1},!q&&{onDelete:De(e)}),getListboxProps:()=>({role:"listbox",id:`${B}-listbox`,"aria-labelledby":`${B}-label`,ref:Ce,onMouseDown:e=>{e.preventDefault()}}),getOptionProps:({index:e,option:t})=>{const o=(M?ae:[ae]).some((e=>null!=e&&O(t,e))),a=!!I&&I(t);return{key:G(t),tabIndex:-1,role:"option",id:`${B}-option-${e}`,onMouseOver:Ae,onClick:Ne,onTouchStart:Fe,"data-option-index":e,"aria-disabled":a,"aria-selected":o}},id:B,inputValue:ne,value:ae,dirty:He,popupOpen:fe,focused:ie||-1!==Y,anchorEl:Q,setAnchorEl:X,focusedTag:Y,groupedOptions:We}}var b=o(94780),f=o(41796),h=o(33999),v=o(90948),y=o(16122),x=o(98216),Z=o(34867),$=o(1588);function C(e){return(0,Z.Z)("MuiListSubheader",e)}(0,$.Z)("MuiListSubheader",["root","colorPrimary","colorInherit","gutters","inset","sticky"]);var S=o(85893);const I=["className","color","component","disableGutters","disableSticky","inset"],k=(0,v.ZP)("li",{name:"MuiListSubheader",slot:"Root",overridesResolver:(e,t)=>{const{ownerState:o}=e;return[t.root,"default"!==o.color&&t[`color${(0,x.Z)(o.color)}`],!o.disableGutters&&t.gutters,o.inset&&t.inset,!o.disableSticky&&t.sticky]}})((({theme:e,ownerState:t})=>(0,r.Z)({boxSizing:"border-box",lineHeight:"48px",listStyle:"none",color:(e.vars||e).palette.text.secondary,fontFamily:e.typography.fontFamily,fontWeight:e.typography.fontWeightMedium,fontSize:e.typography.pxToRem(14)},"primary"===t.color&&{color:(e.vars||e).palette.primary.main},"inherit"===t.color&&{color:"inherit"},!t.disableGutters&&{paddingLeft:16,paddingRight:16},t.inset&&{paddingLeft:72},!t.disableSticky&&{position:"sticky",top:0,zIndex:1,backgroundColor:(e.vars||e).palette.background.paper})));var O=n.forwardRef((function(e,t){const o=(0,y.Z)({props:e,name:"MuiListSubheader"}),{className:n,color:i="default",component:s="li",disableGutters:c=!1,disableSticky:p=!1,inset:d=!1}=o,u=(0,a.Z)(o,I),g=(0,r.Z)({},o,{color:i,component:s,disableGutters:c,disableSticky:p,inset:d}),m=(e=>{const{classes:t,color:o,disableGutters:a,inset:r,disableSticky:n}=e,l={root:["root","default"!==o&&`color${(0,x.Z)(o)}`,!a&&"gutters",r&&"inset",!n&&"sticky"]};return(0,b.Z)(l,C,t)})(g);return(0,S.jsx)(k,(0,r.Z)({as:s,className:(0,l.Z)(m.root,n),ref:t,ownerState:g},u))})),w=o(36501),P=o(6867),R=o(82066),L=(0,R.Z)((0,S.jsx)("path",{d:"M12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm5 13.59L15.59 17 12 13.41 8.41 17 7 15.59 10.59 12 7 8.41 8.41 7 12 10.59 15.59 7 17 8.41 13.41 12 17 15.59z"}),"Cancel"),z=o(51705),M=o(60539);function T(e){return(0,Z.Z)("MuiChip",e)}var A=(0,$.Z)("MuiChip",["root","sizeSmall","sizeMedium","colorPrimary","colorSecondary","disabled","clickable","clickableColorPrimary","clickableColorSecondary","deletable","deletableColorPrimary","deletableColorSecondary","outlined","filled","outlinedPrimary","outlinedSecondary","avatar","avatarSmall","avatarMedium","avatarColorPrimary","avatarColorSecondary","icon","iconSmall","iconMedium","iconColorPrimary","iconColorSecondary","label","labelSmall","labelMedium","deleteIcon","deleteIconSmall","deleteIconMedium","deleteIconColorPrimary","deleteIconColorSecondary","deleteIconOutlinedColorPrimary","deleteIconOutlinedColorSecondary","focusVisible"]);const F=["avatar","className","clickable","color","component","deleteIcon","disabled","icon","label","onClick","onDelete","onKeyDown","onKeyUp","size","variant"],N=(0,v.ZP)("div",{name:"MuiChip",slot:"Root",overridesResolver:(e,t)=>{const{ownerState:o}=e,{color:a,clickable:r,onDelete:n,size:l,variant:i}=o;return[{[`& .${A.avatar}`]:t.avatar},{[`& .${A.avatar}`]:t[`avatar${(0,x.Z)(l)}`]},{[`& .${A.avatar}`]:t[`avatarColor${(0,x.Z)(a)}`]},{[`& .${A.icon}`]:t.icon},{[`& .${A.icon}`]:t[`icon${(0,x.Z)(l)}`]},{[`& .${A.icon}`]:t[`iconColor${(0,x.Z)(a)}`]},{[`& .${A.deleteIcon}`]:t.deleteIcon},{[`& .${A.deleteIcon}`]:t[`deleteIcon${(0,x.Z)(l)}`]},{[`& .${A.deleteIcon}`]:t[`deleteIconColor${(0,x.Z)(a)}`]},{[`& .${A.deleteIcon}`]:t[`deleteIconOutlinedColor${(0,x.Z)(a)}`]},t.root,t[`size${(0,x.Z)(l)}`],t[`color${(0,x.Z)(a)}`],r&&t.clickable,r&&"default"!==a&&t[`clickableColor${(0,x.Z)(a)})`],n&&t.deletable,n&&"default"!==a&&t[`deletableColor${(0,x.Z)(a)}`],t[i],"outlined"===i&&t[`outlined${(0,x.Z)(a)}`]]}})((({theme:e,ownerState:t})=>{const o=(0,f.Fq)(e.palette.text.primary,.26);return(0,r.Z)({maxWidth:"100%",fontFamily:e.typography.fontFamily,fontSize:e.typography.pxToRem(13),display:"inline-flex",alignItems:"center",justifyContent:"center",height:32,color:e.palette.text.primary,backgroundColor:e.palette.action.selected,borderRadius:16,whiteSpace:"nowrap",transition:e.transitions.create(["background-color","box-shadow"]),cursor:"default",outline:0,textDecoration:"none",border:0,padding:0,verticalAlign:"middle",boxSizing:"border-box",[`&.${A.disabled}`]:{opacity:e.palette.action.disabledOpacity,pointerEvents:"none"},[`& .${A.avatar}`]:{marginLeft:5,marginRight:-6,width:24,height:24,color:"light"===e.palette.mode?e.palette.grey[700]:e.palette.grey[300],fontSize:e.typography.pxToRem(12)},[`& .${A.avatarColorPrimary}`]:{color:e.palette.primary.contrastText,backgroundColor:e.palette.primary.dark},[`& .${A.avatarColorSecondary}`]:{color:e.palette.secondary.contrastText,backgroundColor:e.palette.secondary.dark},[`& .${A.avatarSmall}`]:{marginLeft:4,marginRight:-4,width:18,height:18,fontSize:e.typography.pxToRem(10)},[`& .${A.icon}`]:(0,r.Z)({color:"light"===e.palette.mode?e.palette.grey[700]:e.palette.grey[300],marginLeft:5,marginRight:-6},"small"===t.size&&{fontSize:18,marginLeft:4,marginRight:-4},"default"!==t.color&&{color:"inherit"}),[`& .${A.deleteIcon}`]:(0,r.Z)({WebkitTapHighlightColor:"transparent",color:o,fontSize:22,cursor:"pointer",margin:"0 5px 0 -6px","&:hover":{color:(0,f.Fq)(o,.4)}},"small"===t.size&&{fontSize:16,marginRight:4,marginLeft:-4},"default"!==t.color&&{color:(0,f.Fq)(e.palette[t.color].contrastText,.7),"&:hover, &:active":{color:e.palette[t.color].contrastText}})},"small"===t.size&&{height:24},"default"!==t.color&&{backgroundColor:e.palette[t.color].main,color:e.palette[t.color].contrastText},t.onDelete&&{[`&.${A.focusVisible}`]:{backgroundColor:(0,f.Fq)(e.palette.action.selected,e.palette.action.selectedOpacity+e.palette.action.focusOpacity)}},t.onDelete&&"default"!==t.color&&{[`&.${A.focusVisible}`]:{backgroundColor:e.palette[t.color].dark}})}),(({theme:e,ownerState:t})=>(0,r.Z)({},t.clickable&&{userSelect:"none",WebkitTapHighlightColor:"transparent",cursor:"pointer","&:hover":{backgroundColor:(0,f.Fq)(e.palette.action.selected,e.palette.action.selectedOpacity+e.palette.action.hoverOpacity)},[`&.${A.focusVisible}`]:{backgroundColor:(0,f.Fq)(e.palette.action.selected,e.palette.action.selectedOpacity+e.palette.action.focusOpacity)},"&:active":{boxShadow:e.shadows[1]}},t.clickable&&"default"!==t.color&&{[`&:hover, &.${A.focusVisible}`]:{backgroundColor:e.palette[t.color].dark}})),(({theme:e,ownerState:t})=>(0,r.Z)({},"outlined"===t.variant&&{backgroundColor:"transparent",border:`1px solid ${"light"===e.palette.mode?e.palette.grey[400]:e.palette.grey[700]}`,[`&.${A.clickable}:hover`]:{backgroundColor:e.palette.action.hover},[`&.${A.focusVisible}`]:{backgroundColor:e.palette.action.focus},[`& .${A.avatar}`]:{marginLeft:4},[`& .${A.avatarSmall}`]:{marginLeft:2},[`& .${A.icon}`]:{marginLeft:4},[`& .${A.iconSmall}`]:{marginLeft:2},[`& .${A.deleteIcon}`]:{marginRight:5},[`& .${A.deleteIconSmall}`]:{marginRight:3}},"outlined"===t.variant&&"default"!==t.color&&{color:e.palette[t.color].main,border:`1px solid ${(0,f.Fq)(e.palette[t.color].main,.7)}`,[`&.${A.clickable}:hover`]:{backgroundColor:(0,f.Fq)(e.palette[t.color].main,e.palette.action.hoverOpacity)},[`&.${A.focusVisible}`]:{backgroundColor:(0,f.Fq)(e.palette[t.color].main,e.palette.action.focusOpacity)},[`& .${A.deleteIcon}`]:{color:(0,f.Fq)(e.palette[t.color].main,.7),"&:hover, &:active":{color:e.palette[t.color].main}}}))),D=(0,v.ZP)("span",{name:"MuiChip",slot:"Label",overridesResolver:(e,t)=>{const{ownerState:o}=e,{size:a}=o;return[t.label,t[`label${(0,x.Z)(a)}`]]}})((({ownerState:e})=>(0,r.Z)({overflow:"hidden",textOverflow:"ellipsis",paddingLeft:12,paddingRight:12,whiteSpace:"nowrap"},"small"===e.size&&{paddingLeft:8,paddingRight:8})));function V(e){return"Backspace"===e.key||"Delete"===e.key}var E=n.forwardRef((function(e,t){const o=(0,y.Z)({props:e,name:"MuiChip"}),{avatar:i,className:s,clickable:c,color:p="default",component:d,deleteIcon:u,disabled:g=!1,icon:m,label:f,onClick:h,onDelete:v,onKeyDown:Z,onKeyUp:$,size:C="medium",variant:I="filled"}=o,k=(0,a.Z)(o,F),O=n.useRef(null),w=(0,z.Z)(O,t),P=e=>{e.stopPropagation(),v&&v(e)},R=!(!1===c||!h)||c,A="small"===C,E=R||v?M.Z:d||"div",j=(0,r.Z)({},o,{component:E,disabled:g,size:C,color:p,onDelete:!!v,clickable:R,variant:I}),q=(e=>{const{classes:t,disabled:o,size:a,color:r,onDelete:n,clickable:l,variant:i}=e,s={root:["root",i,o&&"disabled",`size${(0,x.Z)(a)}`,`color${(0,x.Z)(r)}`,l&&"clickable",l&&`clickableColor${(0,x.Z)(r)}`,n&&"deletable",n&&`deletableColor${(0,x.Z)(r)}`,`${i}${(0,x.Z)(r)}`],label:["label",`label${(0,x.Z)(a)}`],avatar:["avatar",`avatar${(0,x.Z)(a)}`,`avatarColor${(0,x.Z)(r)}`],icon:["icon",`icon${(0,x.Z)(a)}`,`iconColor${(0,x.Z)(r)}`],deleteIcon:["deleteIcon",`deleteIcon${(0,x.Z)(a)}`,`deleteIconColor${(0,x.Z)(r)}`,`deleteIconOutlinedColor${(0,x.Z)(r)}`]};return(0,b.Z)(s,T,t)})(j),H=E===M.Z?(0,r.Z)({component:d||"div",focusVisibleClassName:q.focusVisible},v&&{disableRipple:!0}):{};let W=null;if(v){const e=(0,l.Z)("default"!==p&&("outlined"===I?q[`deleteIconOutlinedColor${(0,x.Z)(p)}`]:q[`deleteIconColor${(0,x.Z)(p)}`]),A&&q.deleteIconSmall);W=u&&n.isValidElement(u)?n.cloneElement(u,{className:(0,l.Z)(u.props.className,q.deleteIcon,e),onClick:P}):(0,S.jsx)(L,{className:(0,l.Z)(q.deleteIcon,e),onClick:P})}let B=null;i&&n.isValidElement(i)&&(B=n.cloneElement(i,{className:(0,l.Z)(q.avatar,i.props.className)}));let G=null;return m&&n.isValidElement(m)&&(G=n.cloneElement(m,{className:(0,l.Z)(q.icon,m.props.className)})),(0,S.jsxs)(N,(0,r.Z)({as:E,className:(0,l.Z)(q.root,s),disabled:!(!R||!g)||void 0,onClick:h,onKeyDown:e=>{e.currentTarget===e.target&&V(e)&&e.preventDefault(),Z&&Z(e)},onKeyUp:e=>{e.currentTarget===e.target&&(v&&V(e)?v(e):"Escape"===e.key&&O.current&&O.current.blur()),$&&$(e)},ref:w,ownerState:j},H,k,{children:[B||G,(0,S.jsx)(D,{className:(0,l.Z)(q.label),ownerState:j,children:f}),W]}))})),j=o(7021),q=o(55827),H=o(54656),W=o(24707),B=(0,R.Z)((0,S.jsx)("path",{d:"M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"}),"Close"),G=o(60224);function K(e){return(0,Z.Z)("MuiAutocomplete",e)}var U,_,J=(0,$.Z)("MuiAutocomplete",["root","fullWidth","focused","focusVisible","tag","tagSizeSmall","tagSizeMedium","hasPopupIcon","hasClearIcon","inputRoot","input","inputFocused","endAdornment","clearIndicator","popupIndicator","popupIndicatorOpen","popper","popperDisablePortal","paper","listbox","loading","noOptions","option","groupLabel","groupUl"]);const Q=["autoComplete","autoHighlight","autoSelect","blurOnSelect","ChipProps","className","clearIcon","clearOnBlur","clearOnEscape","clearText","closeText","componentsProps","defaultValue","disableClearable","disableCloseOnSelect","disabled","disabledItemsFocusable","disableListWrap","disablePortal","filterOptions","filterSelectedOptions","forcePopupIcon","freeSolo","fullWidth","getLimitTagsText","getOptionDisabled","getOptionLabel","isOptionEqualToValue","groupBy","handleHomeEndKeys","id","includeInputInList","inputValue","limitTags","ListboxComponent","ListboxProps","loading","loadingText","multiple","noOptionsText","onChange","onClose","onHighlightChange","onInputChange","onOpen","open","openOnFocus","openText","options","PaperComponent","PopperComponent","popupIcon","readOnly","renderGroup","renderInput","renderOption","renderTags","selectOnFocus","size","value"],X=(0,v.ZP)("div",{name:"MuiAutocomplete",slot:"Root",overridesResolver:(e,t)=>{const{ownerState:o}=e,{fullWidth:a,hasClearIcon:r,hasPopupIcon:n,inputFocused:l,size:i}=o;return[{[`& .${J.tag}`]:t.tag},{[`& .${J.tag}`]:t[`tagSize${(0,x.Z)(i)}`]},{[`& .${J.inputRoot}`]:t.inputRoot},{[`& .${J.input}`]:t.input},{[`& .${J.input}`]:l&&t.inputFocused},t.root,a&&t.fullWidth,n&&t.hasPopupIcon,r&&t.hasClearIcon]}})((({ownerState:e})=>(0,r.Z)({[`&.${J.focused} .${J.clearIndicator}`]:{visibility:"visible"},"@media (pointer: fine)":{[`&:hover .${J.clearIndicator}`]:{visibility:"visible"}}},e.fullWidth&&{width:"100%"},{[`& .${J.tag}`]:(0,r.Z)({margin:3,maxWidth:"calc(100% - 6px)"},"small"===e.size&&{margin:2,maxWidth:"calc(100% - 4px)"}),[`& .${J.inputRoot}`]:{flexWrap:"wrap",[`.${J.hasPopupIcon}&, .${J.hasClearIcon}&`]:{paddingRight:30},[`.${J.hasPopupIcon}.${J.hasClearIcon}&`]:{paddingRight:56},[`& .${J.input}`]:{width:0,minWidth:30}},[`& .${j.Z.root}`]:{paddingBottom:1,"& .MuiInput-input":{padding:"4px 4px 4px 0px"}},[`& .${j.Z.root}.${q.Z.sizeSmall}`]:{[`& .${j.Z.input}`]:{padding:"2px 4px 3px 0"}},[`& .${H.Z.root}`]:{padding:9,[`.${J.hasPopupIcon}&, .${J.hasClearIcon}&`]:{paddingRight:39},[`.${J.hasPopupIcon}.${J.hasClearIcon}&`]:{paddingRight:65},[`& .${J.input}`]:{padding:"7.5px 4px 7.5px 6px"},[`& .${J.endAdornment}`]:{right:9}},[`& .${H.Z.root}.${q.Z.sizeSmall}`]:{padding:6,[`& .${J.input}`]:{padding:"2.5px 4px 2.5px 6px"}},[`& .${W.Z.root}`]:{paddingTop:19,paddingLeft:8,[`.${J.hasPopupIcon}&, .${J.hasClearIcon}&`]:{paddingRight:39},[`.${J.hasPopupIcon}.${J.hasClearIcon}&`]:{paddingRight:65},[`& .${W.Z.input}`]:{padding:"7px 4px"},[`& .${J.endAdornment}`]:{right:9}},[`& .${W.Z.root}.${q.Z.sizeSmall}`]:{paddingBottom:1,[`& .${W.Z.input}`]:{padding:"2.5px 4px"}},[`& .${q.Z.hiddenLabel}`]:{paddingTop:8},[`& .${J.input}`]:(0,r.Z)({flexGrow:1,textOverflow:"ellipsis",opacity:0},e.inputFocused&&{opacity:1})}))),Y=(0,v.ZP)("div",{name:"MuiAutocomplete",slot:"EndAdornment",overridesResolver:(e,t)=>t.endAdornment})({position:"absolute",right:0,top:"calc(50% - 14px)"}),ee=(0,v.ZP)(P.Z,{name:"MuiAutocomplete",slot:"ClearIndicator",overridesResolver:(e,t)=>t.clearIndicator})({marginRight:-2,padding:4,visibility:"hidden"}),te=(0,v.ZP)(P.Z,{name:"MuiAutocomplete",slot:"PopupIndicator",overridesResolver:({ownerState:e},t)=>(0,r.Z)({},t.popupIndicator,e.popupOpen&&t.popupIndicatorOpen)})((({ownerState:e})=>(0,r.Z)({padding:2,marginRight:-2},e.popupOpen&&{transform:"rotate(180deg)"}))),oe=(0,v.ZP)(h.Z,{name:"MuiAutocomplete",slot:"Popper",overridesResolver:(e,t)=>{const{ownerState:o}=e;return[{[`& .${J.option}`]:t.option},t.popper,o.disablePortal&&t.popperDisablePortal]}})((({theme:e,ownerState:t})=>(0,r.Z)({zIndex:(e.vars||e).zIndex.modal},t.disablePortal&&{position:"absolute"}))),ae=(0,v.ZP)(w.Z,{name:"MuiAutocomplete",slot:"Paper",overridesResolver:(e,t)=>t.paper})((({theme:e})=>(0,r.Z)({},e.typography.body1,{overflow:"auto"}))),re=(0,v.ZP)("div",{name:"MuiAutocomplete",slot:"Loading",overridesResolver:(e,t)=>t.loading})((({theme:e})=>({color:(e.vars||e).palette.text.secondary,padding:"14px 16px"}))),ne=(0,v.ZP)("div",{name:"MuiAutocomplete",slot:"NoOptions",overridesResolver:(e,t)=>t.noOptions})((({theme:e})=>({color:(e.vars||e).palette.text.secondary,padding:"14px 16px"}))),le=(0,v.ZP)("div",{name:"MuiAutocomplete",slot:"Listbox",overridesResolver:(e,t)=>t.listbox})((({theme:e})=>({listStyle:"none",margin:0,padding:"8px 0",maxHeight:"40vh",overflow:"auto",[`& .${J.option}`]:{minHeight:48,display:"flex",overflow:"hidden",justifyContent:"flex-start",alignItems:"center",cursor:"pointer",paddingTop:6,boxSizing:"border-box",outline:"0",WebkitTapHighlightColor:"transparent",paddingBottom:6,paddingLeft:16,paddingRight:16,[e.breakpoints.up("sm")]:{minHeight:"auto"},[`&.${J.focused}`]:{backgroundColor:(e.vars||e).palette.action.hover,"@media (hover: none)":{backgroundColor:"transparent"}},'&[aria-disabled="true"]':{opacity:(e.vars||e).palette.action.disabledOpacity,pointerEvents:"none"},[`&.${J.focusVisible}`]:{backgroundColor:(e.vars||e).palette.action.focus},'&[aria-selected="true"]':{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / ${e.vars.palette.action.selectedOpacity})`:(0,f.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity),[`&.${J.focused}`]:{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / calc(${e.vars.palette.action.selectedOpacity} + ${e.vars.palette.action.hoverOpacity}))`:(0,f.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity+e.palette.action.hoverOpacity),"@media (hover: none)":{backgroundColor:(e.vars||e).palette.action.selected}},[`&.${J.focusVisible}`]:{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / calc(${e.vars.palette.action.selectedOpacity} + ${e.vars.palette.action.focusOpacity}))`:(0,f.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity+e.palette.action.focusOpacity)}}}}))),ie=(0,v.ZP)(O,{name:"MuiAutocomplete",slot:"GroupLabel",overridesResolver:(e,t)=>t.groupLabel})((({theme:e})=>({backgroundColor:(e.vars||e).palette.background.paper,top:-8}))),se=(0,v.ZP)("ul",{name:"MuiAutocomplete",slot:"GroupUl",overridesResolver:(e,t)=>t.groupUl})({padding:0,[`& .${J.option}`]:{paddingLeft:24}});var ce=n.forwardRef((function(e,t){var o,i;const s=(0,y.Z)({props:e,name:"MuiAutocomplete"}),{autoComplete:c=!1,autoHighlight:p=!1,autoSelect:d=!1,blurOnSelect:u=!1,ChipProps:g,className:f,clearIcon:v=U||(U=(0,S.jsx)(B,{fontSize:"small"})),clearOnBlur:Z=!s.freeSolo,clearOnEscape:$=!1,clearText:C="Clear",closeText:I="Close",componentsProps:k={},defaultValue:O=(s.multiple?[]:null),disableClearable:P=!1,disableCloseOnSelect:R=!1,disabled:L=!1,disabledItemsFocusable:z=!1,disableListWrap:M=!1,disablePortal:T=!1,filterSelectedOptions:A=!1,forcePopupIcon:F="auto",freeSolo:N=!1,fullWidth:D=!1,getLimitTagsText:V=(e=>`+${e}`),getOptionLabel:j=(e=>{var t;return null!=(t=e.label)?t:e}),groupBy:q,handleHomeEndKeys:H=!s.freeSolo,includeInputInList:W=!1,limitTags:J=-1,ListboxComponent:ce="ul",ListboxProps:pe,loading:de=!1,loadingText:ue="Loading\u2026",multiple:ge=!1,noOptionsText:me="No options",openOnFocus:be=!1,openText:fe="Open",PaperComponent:he=w.Z,PopperComponent:ve=h.Z,popupIcon:ye=_||(_=(0,S.jsx)(G.Z,{})),readOnly:xe=!1,renderGroup:Ze,renderInput:$e,renderOption:Ce,renderTags:Se,selectOnFocus:Ie=!s.freeSolo,size:ke="medium"}=s,Oe=(0,a.Z)(s,Q),{getRootProps:we,getInputProps:Pe,getInputLabelProps:Re,getPopupIndicatorProps:Le,getClearProps:ze,getTagProps:Me,getListboxProps:Te,getOptionProps:Ae,value:Fe,dirty:Ne,id:De,popupOpen:Ve,focused:Ee,focusedTag:je,anchorEl:qe,setAnchorEl:He,inputValue:We,groupedOptions:Be}=m((0,r.Z)({},s,{componentName:"Autocomplete"})),Ge=!P&&!L&&Ne&&!xe,Ke=(!N||!0===F)&&!1!==F,Ue=(0,r.Z)({},s,{disablePortal:T,focused:Ee,fullWidth:D,hasClearIcon:Ge,hasPopupIcon:Ke,inputFocused:-1===je,popupOpen:Ve,size:ke}),_e=(e=>{const{classes:t,disablePortal:o,focused:a,fullWidth:r,hasClearIcon:n,hasPopupIcon:l,inputFocused:i,popupOpen:s,size:c}=e,p={root:["root",a&&"focused",r&&"fullWidth",n&&"hasClearIcon",l&&"hasPopupIcon"],inputRoot:["inputRoot"],input:["input",i&&"inputFocused"],tag:["tag",`tagSize${(0,x.Z)(c)}`],endAdornment:["endAdornment"],clearIndicator:["clearIndicator"],popupIndicator:["popupIndicator",s&&"popupIndicatorOpen"],popper:["popper",o&&"popperDisablePortal"],paper:["paper"],listbox:["listbox"],loading:["loading"],noOptions:["noOptions"],option:["option"],groupLabel:["groupLabel"],groupUl:["groupUl"]};return(0,b.Z)(p,K,t)})(Ue);let Je;if(ge&&Fe.length>0){const e=e=>(0,r.Z)({className:(0,l.Z)(_e.tag),disabled:L},Me(e));Je=Se?Se(Fe,e,Ue):Fe.map(((t,o)=>(0,S.jsx)(E,(0,r.Z)({label:j(t),size:ke},e({index:o}),g))))}if(J>-1&&Array.isArray(Je)){const e=Je.length-J;!Ee&&e>0&&(Je=Je.splice(0,J),Je.push((0,S.jsx)("span",{className:_e.tag,children:V(e)},Je.length)))}const Qe=Ze||(e=>(0,S.jsxs)("li",{children:[(0,S.jsx)(ie,{className:_e.groupLabel,ownerState:Ue,component:"div",children:e.group}),(0,S.jsx)(se,{className:_e.groupUl,ownerState:Ue,children:e.children})]},e.key)),Xe=Ce||((e,t)=>(0,S.jsx)("li",(0,r.Z)({},e,{children:j(t)}))),Ye=(e,t)=>{const o=Ae({option:e,index:t});return Xe((0,r.Z)({},o,{className:_e.option}),e,{selected:o["aria-selected"],inputValue:We})};return(0,S.jsxs)(n.Fragment,{children:[(0,S.jsx)(X,(0,r.Z)({ref:t,className:(0,l.Z)(_e.root,f),ownerState:Ue},we(Oe),{children:$e({id:De,disabled:L,fullWidth:!0,size:"small"===ke?"small":void 0,InputLabelProps:Re(),InputProps:(0,r.Z)({ref:He,className:_e.inputRoot,startAdornment:Je},(Ge||Ke)&&{endAdornment:(0,S.jsxs)(Y,{className:_e.endAdornment,ownerState:Ue,children:[Ge?(0,S.jsx)(ee,(0,r.Z)({},ze(),{"aria-label":C,title:C,ownerState:Ue},k.clearIndicator,{className:(0,l.Z)(_e.clearIndicator,null==(o=k.clearIndicator)?void 0:o.className),children:v})):null,Ke?(0,S.jsx)(te,(0,r.Z)({},Le(),{disabled:L,"aria-label":Ve?I:fe,title:Ve?I:fe,className:(0,l.Z)(_e.popupIndicator),ownerState:Ue,children:ye})):null]})}),inputProps:(0,r.Z)({className:(0,l.Z)(_e.input),disabled:L,readOnly:xe},Pe())})})),Ve&&qe?(0,S.jsx)(oe,{as:ve,className:(0,l.Z)(_e.popper),disablePortal:T,style:{width:qe?qe.clientWidth:null},ownerState:Ue,role:"presentation",anchorEl:qe,open:!0,children:(0,S.jsxs)(ae,(0,r.Z)({ownerState:Ue,as:he},k.paper,{className:(0,l.Z)(_e.paper,null==(i=k.paper)?void 0:i.className),children:[de&&0===Be.length?(0,S.jsx)(re,{className:_e.loading,ownerState:Ue,children:ue}):null,0!==Be.length||N||de?null:(0,S.jsx)(ne,{className:_e.noOptions,ownerState:Ue,role:"presentation",onMouseDown:e=>{e.preventDefault()},children:me}),Be.length>0?(0,S.jsx)(le,(0,r.Z)({as:ce,className:_e.listbox,ownerState:Ue},Te(),pe,{children:Be.map(((e,t)=>q?Qe({key:e.key,group:e.group,children:e.options.map(((t,o)=>Ye(t,e.index+o)))}):Ye(e,t)))})):null]}))}):null]})}))},6867:function(e,t,o){"use strict";o.d(t,{Z:function(){return y}});var a=o(1048),r=o(32793),n=o(67294),l=o(86010),i=o(94780),s=o(41796),c=o(90948),p=o(16122),d=o(60539),u=o(98216),g=o(34867);function m(e){return(0,g.Z)("MuiIconButton",e)}var b=(0,o(1588).Z)("MuiIconButton",["root","disabled","colorInherit","colorPrimary","colorSecondary","edgeStart","edgeEnd","sizeSmall","sizeMedium","sizeLarge"]),f=o(85893);const h=["edge","children","className","color","disabled","disableFocusRipple","size"],v=(0,c.ZP)(d.Z,{name:"MuiIconButton",slot:"Root",overridesResolver:(e,t)=>{const{ownerState:o}=e;return[t.root,"default"!==o.color&&t[`color${(0,u.Z)(o.color)}`],o.edge&&t[`edge${(0,u.Z)(o.edge)}`],t[`size${(0,u.Z)(o.size)}`]]}})((({theme:e,ownerState:t})=>(0,r.Z)({textAlign:"center",flex:"0 0 auto",fontSize:e.typography.pxToRem(24),padding:8,borderRadius:"50%",overflow:"visible",color:(e.vars||e).palette.action.active,transition:e.transitions.create("background-color",{duration:e.transitions.duration.shortest})},!t.disableRipple&&{"&:hover":{backgroundColor:e.vars?`rgba(${e.vars.palette.action.active} / ${e.vars.palette.action.hoverOpacity})`:(0,s.Fq)(e.palette.action.active,e.palette.action.hoverOpacity),"@media (hover: none)":{backgroundColor:"transparent"}}},"start"===t.edge&&{marginLeft:"small"===t.size?-3:-12},"end"===t.edge&&{marginRight:"small"===t.size?-3:-12})),(({theme:e,ownerState:t})=>(0,r.Z)({},"inherit"===t.color&&{color:"inherit"},"inherit"!==t.color&&"default"!==t.color&&(0,r.Z)({color:(e.vars||e).palette[t.color].main},!t.disableRipple&&{"&:hover":{backgroundColor:e.vars?`rgba(${e.vars.palette[t.color].mainChannel} / ${e.vars.palette.action.hoverOpacity})`:(0,s.Fq)(e.palette[t.color].main,e.palette.action.hoverOpacity),"@media (hover: none)":{backgroundColor:"transparent"}}}),"small"===t.size&&{padding:5,fontSize:e.typography.pxToRem(18)},"large"===t.size&&{padding:12,fontSize:e.typography.pxToRem(28)},{[`&.${b.disabled}`]:{backgroundColor:"transparent",color:(e.vars||e).palette.action.disabled}})));var y=n.forwardRef((function(e,t){const o=(0,p.Z)({props:e,name:"MuiIconButton"}),{edge:n=!1,children:s,className:c,color:d="default",disabled:g=!1,disableFocusRipple:b=!1,size:y="medium"}=o,x=(0,a.Z)(o,h),Z=(0,r.Z)({},o,{edge:n,color:d,disabled:g,disableFocusRipple:b,size:y}),$=(e=>{const{classes:t,disabled:o,color:a,edge:r,size:n}=e,l={root:["root",o&&"disabled","default"!==a&&`color${(0,u.Z)(a)}`,r&&`edge${(0,u.Z)(r)}`,`size${(0,u.Z)(n)}`]};return(0,i.Z)(l,m,t)})(Z);return(0,f.jsx)(v,(0,r.Z)({className:(0,l.Z)($.root,c),centerRipple:!0,focusRipple:!b,disabled:g,ref:t,ownerState:Z},x,{children:s}))}))},26336:function(e,t,o){"use strict";o.d(t,{L:function(){return r}});var a=o(34867);function r(e){return(0,a.Z)("MuiListItemText",e)}const n=(0,o(1588).Z)("MuiListItemText",["root","multiline","dense","inset","primary","secondary"]);t.Z=n},21275:function(e,t,o){"use strict";o.d(t,{Z:function(){return I}});var a=o(1048),r=o(32793),n=o(67294),l=o(86010),i=o(94780),s=o(41796),c=o(90948),p=o(16122),d=o(59773),u=o(60539),g=o(58974),m=o(51705),b=o(1588);var f=(0,b.Z)("MuiDivider",["root","absolute","fullWidth","inset","middle","flexItem","light","vertical","withChildren","withChildrenVertical","textAlignRight","textAlignLeft","wrapper","wrapperVertical"]);var h=(0,b.Z)("MuiListItemIcon",["root","alignItemsFlexStart"]),v=o(26336),y=o(34867);function x(e){return(0,y.Z)("MuiMenuItem",e)}var Z=(0,b.Z)("MuiMenuItem",["root","focusVisible","dense","disabled","divider","gutters","selected"]),$=o(85893);const C=["autoFocus","component","dense","divider","disableGutters","focusVisibleClassName","role","tabIndex"],S=(0,c.ZP)(u.Z,{shouldForwardProp:e=>(0,c.FO)(e)||"classes"===e,name:"MuiMenuItem",slot:"Root",overridesResolver:(e,t)=>{const{ownerState:o}=e;return[t.root,o.dense&&t.dense,o.divider&&t.divider,!o.disableGutters&&t.gutters]}})((({theme:e,ownerState:t})=>(0,r.Z)({},e.typography.body1,{display:"flex",justifyContent:"flex-start",alignItems:"center",position:"relative",textDecoration:"none",minHeight:48,paddingTop:6,paddingBottom:6,boxSizing:"border-box",whiteSpace:"nowrap"},!t.disableGutters&&{paddingLeft:16,paddingRight:16},t.divider&&{borderBottom:`1px solid ${(e.vars||e).palette.divider}`,backgroundClip:"padding-box"},{"&:hover":{textDecoration:"none",backgroundColor:(e.vars||e).palette.action.hover,"@media (hover: none)":{backgroundColor:"transparent"}},[`&.${Z.selected}`]:{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / ${e.vars.palette.action.selectedOpacity})`:(0,s.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity),[`&.${Z.focusVisible}`]:{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / calc(${e.vars.palette.action.selectedOpacity} + ${e.vars.palette.action.focusOpacity}))`:(0,s.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity+e.palette.action.focusOpacity)}},[`&.${Z.selected}:hover`]:{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / calc(${e.vars.palette.action.selectedOpacity} + ${e.vars.palette.action.hoverOpacity}))`:(0,s.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity+e.palette.action.hoverOpacity),"@media (hover: none)":{backgroundColor:e.vars?`rgba(${e.vars.palette.primary.mainChannel} / ${e.vars.palette.action.selectedOpacity})`:(0,s.Fq)(e.palette.primary.main,e.palette.action.selectedOpacity)}},[`&.${Z.focusVisible}`]:{backgroundColor:(e.vars||e).palette.action.focus},[`&.${Z.disabled}`]:{opacity:(e.vars||e).palette.action.disabledOpacity},[`& + .${f.root}`]:{marginTop:e.spacing(1),marginBottom:e.spacing(1)},[`& + .${f.inset}`]:{marginLeft:52},[`& .${v.Z.root}`]:{marginTop:0,marginBottom:0},[`& .${v.Z.inset}`]:{paddingLeft:36},[`& .${h.root}`]:{minWidth:36}},!t.dense&&{[e.breakpoints.up("sm")]:{minHeight:"auto"}},t.dense&&(0,r.Z)({minHeight:32,paddingTop:4,paddingBottom:4},e.typography.body2,{[`& .${h.root} svg`]:{fontSize:"1.25rem"}}))));var I=n.forwardRef((function(e,t){const o=(0,p.Z)({props:e,name:"MuiMenuItem"}),{autoFocus:s=!1,component:c="li",dense:u=!1,divider:b=!1,disableGutters:f=!1,focusVisibleClassName:h,role:v="menuitem",tabIndex:y}=o,Z=(0,a.Z)(o,C),I=n.useContext(d.Z),k={dense:u||I.dense||!1,disableGutters:f},O=n.useRef(null);(0,g.Z)((()=>{s&&O.current&&O.current.focus()}),[s]);const w=(0,r.Z)({},o,{dense:k.dense,divider:b,disableGutters:f}),P=(e=>{const{disabled:t,dense:o,divider:a,disableGutters:n,selected:l,classes:s}=e,c={root:["root",o&&"dense",t&&"disabled",!n&&"gutters",a&&"divider",l&&"selected"]},p=(0,i.Z)(c,x,s);return(0,r.Z)({},s,p)})(o),R=(0,m.Z)(O,t);let L;return o.disabled||(L=void 0!==y?y:-1),(0,$.jsx)(d.Z.Provider,{value:k,children:(0,$.jsx)(S,(0,r.Z)({ref:R,role:v,tabIndex:L,component:c,focusVisibleClassName:(0,l.Z)(P.focusVisible,h)},Z,{ownerState:w,classes:P}))})}))},13235:function(){}}]);