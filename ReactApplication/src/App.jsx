import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import './App.css'
import {
    Routes,
    Route
} from "react-router-dom";
import NavBar2 from './components/navbar';
import Bugs from './pages/bugs';
import Users from './pages/users';

//const test = {
//    testing: 123
//};



//const search = count => new Promise((resolves, rejects) => {
//    const api = 'https://localhost:7040/Bug/54';
//    const request = new XMLHttpRequest();
//    request.open("GET", api);
//    request.onload = () => request.status === 200 ? resolves(JSON.parse(request.response).results) : rejects(Error(request.statusText));
//    request.onerror = err => rejects(err);
//    request.send();
//    console.log(request.response);
//});

const App = () => {
    return (
        <>
        <NavBar2 />
   
            <Container>
                <Row>
                    <Col>
                        <Routes>
                            <Route path="/bugs" element={<Bugs />}></Route>
                            <Route path="/users" element={<Users />}></Route>
                            <Route path="/" element={<Bugs />}></Route>
                        </Routes>
                    </Col>
                </Row>
        </Container>
        </>
    );
}

export default App
