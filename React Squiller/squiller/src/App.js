import React, { Component } from 'react';
import logo from './squillerlogo.svg';
import logoblack from './logoblack.png';
import supreme from './supreme.png';
import lv from './lv.png';
import rolex1 from './rolex1.jpg'
import rolex2 from './rolex2.jpg'
import rolex3 from './rolex3.jpg'
import './App.css';
import {Grid, Row, Col, Button, Thumbnail, Image, Navbar, MenuItem, Nav, NavItem, NavDropdown, Carousel, CarouselItem, CarouselCaption} from 'react-bootstrap';

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <NavBar/>
          <HeaderGrid/>
        </div>
        <div>
        <CarouselSlide/>
        </div>
      </div>
    );
  }
}

class ShoppingList extends React.Component {
  render() {
    return (
      <div>
        <h1>Shopping List for {this.props.name}</h1>
        <ul>
          <li>monies</li>
          <li>supreme</li>
          <li>lv</li>
        </ul>
      </div>
    );
  }
}

class HeaderGrid extends React.Component{
  render(){
    return(
      <Grid>
      <Row className="show-grid">
        <Col xs={6} md={8}><code>&lt;{'Col xs={12} md={8}'} /&gt;</code></Col>
        <Col xs={6} md={4}><code>&lt;{'Col xs={6} md={4}'} /&gt;</code></Col>
      </Row>
      </Grid>
    )
  }
}

class CarouselSlide extends React.Component{
  render(){
    return(
        <Carousel>
          <Carousel.Item>
            <img width={900} height={500} alt="900x500" src={rolex1}/>
            <Carousel.Caption>
              <h3>Rolex Submariner</h3>
              <p>Two tone</p>
            </Carousel.Caption>
          </Carousel.Item>
          <Carousel.Item>
            <img width={900} height={500} alt="900x500" src={rolex2}/>
            <Carousel.Caption>
              <h3>Rolex Datejust</h3>
              <p>White gold</p>
            </Carousel.Caption>
          </Carousel.Item>
          <Carousel.Item>
            <img width={900} height={500} alt="900x500" src={rolex3}/>
            <Carousel.Caption>
              <h3>Rolex Yacht-master II</h3>
              <p> Steel</p>
            </Carousel.Caption>
          </Carousel.Item>
        </Carousel>
    );
  }
}
class NavBar extends React.Component {
  render() {
    return (
      <div>
        <Navbar>
    <Navbar.Header>
      <Navbar.Brand>
      <Image src={logoblack} className="App-logo" alt="logo" />
      </Navbar.Brand>
    </Navbar.Header>
    <Nav>
    <NavDropdown eventKey={1} title="Men" id="basic-nav-dropdown">
        <MenuItem eventKey={1.1}>Action</MenuItem>
        <MenuItem eventKey={1.2}>Another action</MenuItem>
        <MenuItem eventKey={1.3}>Something else here</MenuItem>
        <MenuItem divider />
        <MenuItem eventKey={1.4}>Separated link</MenuItem>
      </NavDropdown>
      <NavDropdown eventKey={2} title="Women" id="basic-nav-dropdown">
        <MenuItem eventKey={2.1}>Action</MenuItem>
        <MenuItem eventKey={2.2}>Another action</MenuItem>
        <MenuItem eventKey={2.3}>Something else here</MenuItem>
        <MenuItem divider />
        <MenuItem eventKey={2.4}>Separated link</MenuItem>
      </NavDropdown>
      <NavDropdown eventKey={3} title="Accessories" id="basic-nav-dropdown">
        <MenuItem eventKey={3.1}>Action</MenuItem>
        <MenuItem eventKey={3.2}>Another action</MenuItem>
        <MenuItem eventKey={3.3}>Something else here</MenuItem>
        <MenuItem divider />
        <MenuItem eventKey={3.4}>Separated link</MenuItem>
      </NavDropdown>
    </Nav>
  </Navbar>
  </div>
    );
  }
}
export default App;
