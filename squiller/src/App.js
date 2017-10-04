import React, { Component } from 'react';
import logowhite from './squillerlogo.png';
import logoblack from './logoblack.png';
import supreme from './supreme.png';
import lv from './lv.png';
import rolex1 from './rolex1.jpg'
import rolex2 from './rolex2.jpg'
import rolex3 from './rolex3.jpg'
import cg1 from './cg1.jpg'
import cg2 from './cg2.jpg'
import cg3 from './cg3.jpg'
import './App.css';
import {Responsive,
        ProgressBar,
        Glyphicon, 
        Modal,
        Collapse, 
        Grid, 
        Row, 
        Col, 
        Button, 
        Thumbnail, 
        Image, 
        Navbar, 
        MenuItem, 
        Nav, 
        NavItem, 
        NavDropdown,
        Carousel, 
        CarouselItem, 
        CarouselCaption} from 'react-bootstrap';

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
        <NavBar/>
        </div>
        <div className="App-body">
          <BodyGrid/>
        </div>
        <div className="App-footer">
          <ProgressBar bsStyle="success" active now={10}/>
          <h3>This site is currently in development.</h3>
        </div>
      </div>
    );
  }
}

class NavBar extends Component {
  render() {
    return (
        <Navbar inverse="true">
    <Navbar.Header>
      <Navbar.Brand>
      <a href="#"><Image src={logowhite} className="App-logo" alt="logo"/></a>
      </Navbar.Brand>
      <Navbar.Toggle/>
    </Navbar.Header>
    <Navbar.Collapse>
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
  <Nav pullRight>
      <NavItem><Glyphicon glyph="shopping-cart"/></NavItem>
  </Nav>
    </Navbar.Collapse>

  </Navbar>
    );
  }
}

class ShoppingList extends Component {
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

class HeaderGrid extends Component{
  render(){
    return(
      <Grid>
      <Row className="show-grid">
        <Col xs={6} md={8}><code>&lt;{'Col xs={12} md={8}'} /&gt;</code></Col>
        <Col xs={6} md={4} mdO><code>&lt;{'Col xs={6} md={4}'} /&gt;</code></Col>
      </Row>
      </Grid>
    )
  }
}

class BodyGrid extends Component{
  render(){
    return(
      <Grid>
        <Row className="show-grid">
          <Col md={12}>
            <CarouselSlide/>
          </Col>
        </Row>
        <Row className="show-grid">
          <Col md={4}>
          <Product img={rolex1} title={'Rolex Submariner'} price={6000}/>&nbsp;
          </Col>
          <Col md={4}>
          <Product img={rolex2} title={'Rolex Datejust 41'} price={7000}/>&nbsp;
          </Col>
          <Col md={4}>
          <Product img={rolex3} title={'Rolex Yacht-master II'} price={8000}/>&nbsp;
          </Col>
          <Col md={4}>
          <Product img={cg1} title={'Canada Goose Expedition Parka'} price={1235}/>&nbsp;
          </Col>
          <Col md={4}>
          <Product img={cg2} title={'Canada Goose Citadel Parka'} price={1089}/>&nbsp;
          </Col>
          <Col md={4}>
          <Product img={cg3} title={'Canada Goose Chateau Parka'} price={999}/>&nbsp;
          </Col>
        </Row>
      </Grid>
    )
  }
}

class Product extends Component{
  render(){
    return(
      <Thumbnail src={this.props.img} className="Thumbnail">
        <h3>{this.props.title}</h3>
        <p>€{this.props.price}</p>
        <p>
          <Button bsStyle="primary">Buy</Button>&nbsp;
          <Button bsStyle="default">Info</Button>
        </p>
      </Thumbnail>
    )
  }
}

class CarouselSlide extends Component{
  render(){
    return(
        <Carousel pauseOnHover="true">
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

export default App;
