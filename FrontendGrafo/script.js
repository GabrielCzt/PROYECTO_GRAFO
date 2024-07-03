const canvas = document.getElementById('grafoCanvas');
const ctx = canvas.getContext('2d');
let vertices = [];
let positions = [];

async function obtenerVertices() {
    const response = await fetch('http://localhost:53113/api/grafo/obtener-vertices');
    vertices = await response.json();
    calcularPosiciones();
    dibujarGrafo();
}

function calcularPosiciones() {
    const centerX = canvas.width / 2;
    const centerY = canvas.height / 2;
    const baseRadius = 100;
    const maxPerCircle = 15;
    const circleIncrement = 50;

    positions = vertices.map((_, index) => {
        const circleIndex = Math.floor(index / maxPerCircle);
        const angle = (2 * Math.PI / Math.min(maxPerCircle, vertices.length - circleIndex * maxPerCircle)) * (index % maxPerCircle);
        const radius = baseRadius + circleIndex * circleIncrement;
        return {
            x: centerX + radius * Math.cos(angle),
            y: centerY + radius * Math.sin(angle)
        };
    });
}

async function agregarVertice() {
    const nombreCiudad = document.getElementById('nombreCiudad').value;
    const habitantes = document.getElementById('habitantes').value;
    const superficie = document.getElementById('superficie').value;

    if (!nombreCiudad || !habitantes || !superficie) {
        alert('Por favor, completa todos los campos para agregar un vértice.');
        return;
    }

    await fetch('http://localhost:53113/api/grafo/agregar-vertice', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            NomCiudad: nombreCiudad,
            totalhabitantes: parseInt(habitantes),
            superficieKm: parseFloat(superficie),
        }),
    });
    obtenerVertices();
}

async function agregarArista() {
    const origen = document.getElementById('origen').value;
    const destino = document.getElementById('destino').value;
    const costo = document.getElementById('costo').value;

    if (!origen || !destino || !costo) {
        alert('Por favor, completa todos los campos para agregar una arista.');
        return;
    }

    await fetch('http://localhost:53113/api/grafo/agregar-arista', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            VertOrign: parseInt(origen),
            VertDest: parseInt(destino),
            cost3: parseFloat(costo),
        }),
    });
    obtenerVertices();
}

function dibujarGrafo() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    vertices.forEach((vertice, index) => {
        const { x, y } = positions[index];

        ctx.fillStyle = 'blue';
        ctx.beginPath();
        ctx.arc(x, y, 20, 0, Math.PI * 2);
        ctx.fill();
        ctx.fillStyle = 'white';
        ctx.fillText(vertice.NomCiudad, x - 10, y + 5);

        fetch(`http://localhost:53113/api/grafo/obtener-aristas/${index}`)
            .then(response => response.json())
            .then(aristas => {
                aristas.forEach(destino => {
                    const destX = positions[destino].x;
                    const destY = positions[destino].y;
                    ctx.beginPath();
                    ctx.moveTo(x, y);
                    ctx.lineTo(destX, destY);
                    ctx.stroke();
                });
            });
    });
}

async function recorridoDFS() {
    const inicio = document.getElementById('inicioDFS').value;
    const response = await fetch(`http://localhost:53113/api/grafo/dfs/${inicio}`);
    const resultado = await response.json();
    const nombres = resultado.map(index => vertices[index].NomCiudad);
    document.getElementById('resultados').innerText = `DFS: ${nombres.join(', ')}`;
}

async function recorridoBFS() {
    const inicio = document.getElementById('inicioBFS').value;
    const response = await fetch(`http://localhost:53113/api/grafo/bfs/${inicio}`);
    const resultado = await response.json();
    const nombres = resultado.map(index => vertices[index].NomCiudad);
    document.getElementById('resultados').innerText = `BFS: ${nombres.join(', ')}`;
}

async function ordenTopologico() {
    const response = await fetch('http://localhost:53113/api/grafo/orden-topologico');
    const resultado = await response.json();
    const nombres = resultado.map(index => vertices[index].NomCiudad);
    document.getElementById('resultados').innerText = `Orden topológico: ${nombres.join(', ')}`;
}

async function ordenTopologicoCamino() {
    const inicio = document.getElementById('inicioTopSort').value;
    const fin = document.getElementById('finTopSort').value;

    if (!inicio || !fin) {
        alert('Por favor, completa ambos campos (Inicio y Fin) para buscar el camino topológico.');
        return;
    }

    const response = await fetch(`http://localhost:53113/api/grafo/orden-topologico-camino/${inicio}/${fin}`);
    const resultado = await response.json();
    const nombres = resultado.map(index => vertices[index].NomCiudad);
    document.getElementById('resultados').innerText = `Orden topológico del camino: ${nombres.join(', ')}`;
}

async function dijkstra() {
    const inicio = document.getElementById('inicioDijkstra').value;
    const fin = document.getElementById('finDijkstra').value;

    if (!inicio || !fin) {
        alert('Por favor, completa ambos campos (Inicio y Fin) para encontrar el camino más corto.');
        return;
    }

    const response = await fetch(`http://localhost:53113/api/grafo/dijkstra/${inicio}/${fin}`);
    const resultado = await response.json();
    const nombres = resultado.map(index => vertices[index].NomCiudad);
    document.getElementById('resultados').innerText = `Dijkstra: ${nombres.join(', ')}`;
}

window.onload = obtenerVertices;

