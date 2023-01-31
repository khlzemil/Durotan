const filterBtn = document.querySelector("#btnFilter");
const filterTable = document.querySelector(".filter-table");
const closeBtn = document.querySelector(".close");

filterBtn.addEventListener('click', ()=>{

  filterTable.style.display = "block";

})

closeBtn.addEventListener('click', ()=>{

  filterTable.style.display = "none";

})