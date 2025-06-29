import { Layout, Select, Space, Button, Drawer } from 'antd';
import { useContext, useState } from 'react';
import { CryptoContext } from '../../context/crypto-context';
import AddCoinForm from './AddCoinForm';
import TelegramWidget from './TelegramWidget';

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
const {prices, wallet, selectCoins, isAuthenticatedб, user} = useContext(CryptoContext)

const handleSelect = (value) =>{
  console.log(value)
}

console.log(isAuthenticated);

    return(
    <Layout.Header style={headerStyle}>
    <Select
      style={{ width: 250 }}
      onSelect={handleSelect}
      value="press / to open"
      options={selectCoins.map((coin) => ({
        label: coin.name,
        value: coin.symbol,
        icon: coin.imageUrl,
      }))}
      optionRender={(option) => (
        <Space>
          <img src={option.data.icon} alt={option.data.label} style={{ width: 20 }} /> {option.data.label}
        </Space>
      )}
    />

  <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
      {!isAuthenticated ? (
        <TelegramWidget />
      ) : (
        <>
          <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
            <img
              src={user?.photo_url}
              alt={user?.first_name}
              style={{ width: '32px', height: '32px', borderRadius: '50%' }}
            />
            <span>{user?.first_name}</span>
          </div>
          <Button type="primary" onClick={() => setDrawer(true)}>Add coin</Button>
        </>
      )}
    </div>

  <Drawer
    width={'20%'}
    title="Add Coin"
    closable={{ 'aria-label': 'Close Button' }}
    onClose={() => setDrawer(false)}
    open={drawer}
    padding={'0rem'}
    destroyOnHidden>
    <AddCoinForm />
  </Drawer>

    </Layout.Header>
)
}