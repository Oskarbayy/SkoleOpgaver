let usePort = 3000;

let express = require('express');
let app = new express();

app.use(express.static('public'));

app.get('/getlisting/:id', (req, res) => {
    let userID = req.params.id;
})


const cards = [
    {
      "id": 1,
      "title": "Moderne villa med havudsigt",
      "city": "2900 Hellerup",
      "street": "Strandvejen 123",
      "thumbnail": "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/600x400/e3b32e25-ac7f-4610-b31e-fc6b7d632d7e.webp",
      "squareMeters": 200,
      "price": 5000000,
      "rooms": 5,
      "description": "Smuk moderne villa med en fantastisk udsigt over havet. Perfekt til familien, der ønsker ro og luksus.",
      "coordinates": {
        "latitude": 55.7321,
        "longitude": 12.5763
      },
      "images": [
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/e3b32e25-ac7f-4610-b31e-fc6b7d632d7e.webp",
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/7dca48a4-8556-4b0e-adf6-5f193b4f9853.webp",
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/4d19de11-7a2b-48dc-b56c-fd4489ddf919.webp"
      ]
    },
    {
      "id": 2,
      "title": "Charmerende bylejlighed",
      "city": "1150 København K",
      "street": "Købmagergade 55",
      "thumbnail": "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/600x400/e3b32e25-ac7f-4610-b31e-fc6b7d632d7e.webp",
      "squareMeters": 85,
      "price": 3000000,
      "rooms": 3,
      "description": "Centralt beliggende lejlighed i hjertet af København. Tæt på alle byens attraktioner og gode transportmuligheder.",
      "coordinates": {
        "latitude": 55.6799,
        "longitude": 12.5736
      },
      "images": [
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/e3b32e25-ac7f-4610-b31e-fc6b7d632d7e.webp",
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/7dca48a4-8556-4b0e-adf6-5f193b4f9853.webp",
        "https://images.boligsiden.dk/images/case/154827e5-d736-4ba3-b8d1-d6ab2049d807/1440x960/4d19de11-7a2b-48dc-b56c-fd4489ddf919.webp"
      ]
    }
  ];

app.get('/getAllListings/', (req, res) => {
    return res.send(cards).end();
})


app.listen(usePort, (e) => {
    console.log(e ? e : `listening on port ${usePort}`);
});