import { createStore } from 'vuex';
import userModule from './modules/user';
import createPersistedState from 'vuex-persistedstate';

export default createStore({
    modules: {
        user: userModule,
    },
    plugins: [createPersistedState()],
});
