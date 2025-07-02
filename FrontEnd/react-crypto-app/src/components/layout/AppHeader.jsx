import { Layout, Select, Space, Button, Drawer } from 'antd';
import { useContext, useState } from 'react';
import { CryptoContext } from '../../context/crypto-context';
import AddCoinForm from './AddCoinForm';
import TelegramWidget from './TelegramWidget';
import userAvatar from '../../assets/default_avatar.png';

const headerStyle = {
  width: '100%',
  textAlign: 'center',
  height: 60,
  padding: '1rem',
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  background: 'white',
  borderBottom: '1px solid #d9d9d9',
};


export default function AppHeader(){
const [drawer, setDrawer] = useState(false)
const {prices, wallet, selectCoins, isAuthenticated, user} = useContext(CryptoContext)

const handleSelect = (value) =>{
  console.log(value)
}
const userDataRaw = localStorage.getItem('userData')
const userData = userDataRaw ? JSON.parse(userDataRaw) : null;

    return(
    <Layout.Header style={headerStyle}>

  <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
    {!isAuthenticated && <TelegramWidget />}
    {isAuthenticated && <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
      <img src={userAvatar}></img>
      <span style={{ fontWeight: '600', fontSize: '16px', color: '#333' }}>{userData?.first_name}</span>
    </div>}
    <>{isAuthenticated && (<Button type="primary" onClick={() => setDrawer(true)}>Add coin</Button>)}</>
  </div>

  <Drawer
    width={'30%'}
    title="Add Coin"
    closable={{ 'aria-label': 'Close Button' }}
    onClose={() => setDrawer(false)}
    open={drawer}
    padding={'0rem'}
    destroyOnHidden>
    <AddCoinForm onClose={() => setDrawer(false)}/>
  </Drawer>

    </Layout.Header>
)
}