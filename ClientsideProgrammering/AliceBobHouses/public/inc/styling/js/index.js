var slideIndex = 1;

function plusDivs(n) {
  showDivs(slideIndex += n);
}

function currentDiv(n) {
  showDivs(slideIndex = n);
}

function showDivs(n) {
  var i;
  var x = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("demo");
  if (n > x.length) {slideIndex = 1}
  if (n < 1) {slideIndex = x.length}
  for (i = 0; i < x.length; i++) {
    x[i].style.display = "none";  
  }
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" w3-white", "");
  }
  x[slideIndex-1].style.display = "block";  
  dots[slideIndex-1].className += " w3-white";
}

function createCard(item) {
  const card = document.createElement('div');
  card.classList.add('card');
  card.setAttribute('data-id', item.id);

  card.innerHTML = `<div class="cardImage">
                        <img src="${item.thumbnail}" onclick="viewListing(${item.id}) style="cursor:pointer;" />
                        <p>${item.street}, ${item.city}</p>
                    </div>
                    <div class="cardDetails">
                        <h3>${item.price}<br><span>M2-pris: ${Math.round(item.price / item.squareMeters)} kr</span></h3>
                        <div class="cardStats">
                            <div>
                                <p><i class="fa fa-home" aria-hidden="true"></i>${item.squareMeters}</p>
                                <p><i class="fa fa-bed" aria-hidden="true"></i>${item.rooms}</p>
                            </div>
                        </div>
                        <div class="clearFix"></div>
                        <div class="cardContact">
                            <div>
                                <button onclick="viewListing(${item.id})">Se bolig</button>
                            </div>
                            <div>
                                <button onclick="contactAgent(${item.id})">Kontakt mælger</button>
                            </div>
                        </div>
                    </div>
                    <div class="clearFix"></div>`;

  return card;
}

let cardData = [];

document.addEventListener('DOMContentLoaded', function () {
  const cardContainer = document.getElementById('listingBody');
  cardContainer.innerHTML = '';

  fetch('/getAllListings')
    .then(response => response.json())
    .then(data => {
      cardData = data;
      data.forEach(cardData => {
        const cardElement = createCard(cardData);
        cardContainer.appendChild(cardElement);
      })
    });
});

document.addEventListener("DOMContentLoaded", function() {
    var searchInput = document.getElementById("searchInput");
    var searchForm = document.getElementById("searchForm");
 
    searchForm.addEventListener("submit", function(event) {
        event.preventDefault();
        var searchTerm = searchInput.value.trim().toLowerCase();

        fetch(`/searchListings?term=${searchTerm}`)
            .then(response => response.json())
            .then(data => {
                const cardContainer = document.getElementById('listingBody');
                cardContainer.innerHTML = ''; // Clear current listings
                data.forEach(cardData => {
                    const cardElement = createCard(cardData);
                    cardContainer.appendChild(cardElement);
                });
            })
            .catch(error => console.error('Error:', error));
    });
});


function viewListing(id) {
  const modal = document.getElementById('modal');
  modal.innerHTML = '';

  const view = document.createElement('div');
  view.id = "listingModal";
  view.classList = "w3-container w3-modal";
  view.style.display = "block";

  const myCardData = cardData[id - 1];

  view.innerHTML = `<div class="w3-modal-content w3-margin-top w3-card-4" style="width: 1000px;">
      <header class="w3-container w3-red"> 
        <span onclick="closeModal()" 
        class="w3-button w3-display-topright">&times;</span>
        <h2>${myCardData.street}</h2>
      </header>
      
      
      <div class="w3-container">

          <!-- slideshow -->
          <div class="w3-content w3-display-container" style="max-width:100%">
              ${createSlideShow(id)}
              <div class="w3-center w3-container w3-section w3-large w3-text-white w3-display-bottommiddle" style="width:100%">
              <div class="w3-left w3-hover-text-khaki" onclick="plusDivs(-1)">&#10094;</div>
              <div class="w3-right w3-hover-text-khaki" onclick="plusDivs(1)">&#10095;</div>
              <span class="w3-badge demo w3-border w3-transparent w3-hover-white" onclick="currentDiv(1)"></span>
              <span class="w3-badge demo w3-border w3-transparent w3-hover-white" onclick="currentDiv(2)"></span>
              <span class="w3-badge demo w3-border w3-transparent w3-hover-white" onclick="currentDiv(3)"></span>
              </div>
          </div>

          <!-- info -->
          <div class="w3-row">
              <div class="w3-half w3-container">
                  <h2>${myCardData.price} kr.</h2>
              </div>
              <div class="w3-half w3-container">
                    <div>
                        <p><i class="fa fa-home" aria-hidden="true"></i>${myCardData.squareMeters}</p>
                        <p><i class="fa fa-bed" aria-hidden="true"></i>${myCardData.rooms}</p>
                    </div>
                    <p>${myCardData.description}</p>
              </div>
          </div>


          <!-- map -->
          <div class="listingModalMap w3-padding-large">
            <iframe 
              width="100%"
              height="350"
              src="https://www.openstreetmap.org/export/embed.html?bbox=${myCardData.coordinates.longitude-0.005},${myCardData.coordinates.latitude-0.005},${myCardData.coordinates.longitude+0.005},${myCardData.coordinates.latitude+0.005}&layer=mapnik&marker=${myCardData.coordinates.latitude},${myCardData.coordinates.longitude}"
              style="border:none">
            </iframe>
          </div>
      </div>
      
    </div>`;

    modal.appendChild(view);

    showDivs(slideIndex);
}

function createSlideShow(id){
  let result = "";
  for (let i = 0; i < cardData[id-1].images.length; i++)
  {
    result += `<img class="mySlides" src="${cardData[id-1].images[i]}" style="width:100%">`
  }
  // let result = cardData[id-1].images.forEach(
  //   (image) => {
  //     `<img class="mySlides" src="${image}" style="width:100%">`
  //   }
  // )

  return result;
}

function closeModal(){
  const modal = document.getElementById('modal');
  if (modal){
    modal.innerHTML = '';
  }
}

function contactAgent(id) {
  const modal = document.getElementById('modal');
  modal.innerHTML = '';

  const view = document.createElement('div');
  view.id = "contactModal";
  view.classList = "w3-container w3-modal";
  view.style.display = "block";

  const myCardData = cardData[id - 1];

  view.innerHTML = `<div class="w3-modal-content w3-card-4" style="width: 800px;">
                <header class="w3-container w3-red"> 
                    <span onclick="closeModal()" class="w3-button w3-display-topright">&times;</span>
                    <h2>Bestil fremvisning til ${myCardData.street}</h2>
                </header>
                
                <div class="w3-container w3-padding">
                    <form class="w3-container" action="mailto:agent@example.com">
                        
                        <input name="listingId" type="text" value="${id-1}" hidden>
                        <input name="esateAddress" type="text" value="${myCardData.street} ${myCardData.city}" hidden>

                        <div class="w3-half w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>First Name</b></label>
                            <input class="w3-input w3-border" name="first" type="text">
                        </div>
                        <div class="w3-rest w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>Last Name</b></label>
                            <input class="w3-input w3-border" name="last" type="text">
                        </div>
            
                        <div class="w3-threequarter w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>Email Address</b></label>
                            <input class="w3-input w3-border" name="email" type="text">
                        </div>
            
                        <div class="w3-rest w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>Phone number</b></label>
                            <input class="w3-input w3-border" name="phone" type="text">
                        </div>
            
                        <div class="w3-quarter w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>Subject</b></label>
                            <input class="w3-input w3-border" name="subject" type="text">
                        </div>
            
                        <div class="w3-rest w3-margin-left w3-margin-right">
                            <label class="w3-text-grey"><b>Message</b></label> 
                            <input class="w3-input w3-border" name="body" type="text">
                        </div>

                        <button class="w3-btn w3-red w3-margin" style="float: right;">Send></button>
                    </form>
                </div>
            </div>`

    modal.appendChild(view);
}

function showConfirmedContact(id){
  const modal = document.getElementById('modal');
  modal.innerHTML = '';

  const view = document.createElement('div');
  view.id = "contactModal";
  view.classList = "w3-container w3-modal";
  view.style.display = "block";

  const myCardData = cardData[id - 1];

  view.innerHTML = `<div class="w3-modal-content w3-card-4" style="width: 800px;">
                <header class="w3-container w3-red"> 
                    <span onclick="closeModal()" class="w3-button w3-display-topright">&times;</span>
                    <h2>Bestil fremvisning til ${myCardData.street}</h2>
                </header>
                
                <div class="w3-container w3-padding">
                    <p></p>
                </div>
            </div>`

    modal.appendChild(view);
}