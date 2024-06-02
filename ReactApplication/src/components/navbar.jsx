import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { NavLink } from 'react-router-dom'

const NavBar = () => {
    return (
        <Navbar  expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Brand as={NavLink} to="/bugs">Bug Tracker</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="justify-content-center" >
                        <Nav.Link as={NavLink} to="/bugs">Bugs</Nav.Link>
                        <Nav.Link as={NavLink} to="/users">Users</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default NavBar;

