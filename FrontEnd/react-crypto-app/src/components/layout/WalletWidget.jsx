import { Layout, Typography } from 'antd';
import { useContext } from 'react';
import { CryptoContext } from '../../context/crypto-context';

const contentStyle = {
  textAlign: 'center',
  height: 'calc(100vh - 60px)',
  color: '#fff',
  backgroundColor: '#f2f5ff',
  padding: '1rem',
};

export default function WalletWidget(){
const {wallet, prices} = useContext(CryptoContext)

console.log(wallet)
    return (
        <Layout.Content>
          <Typography.Title level={3} style={{textAlign: 'left'}}>
            Portfolio: {wallet.map(coin => {
              const price = +(prices.find(p => p.symbol.replace(/USDT$/, '').toLowerCase() === 
              coin.symbol.toLowerCase())?.price)
              return coin.totalAmount * price
            }).reduce((acc, v) => (acc += v), 0).toFixed(2)}$
          </Typography.Title>
        </Layout.Content>
    )
}