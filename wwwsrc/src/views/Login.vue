<template>
    <div class="login">
        <navbar />
        <div class="row">
            <div class="col-12 d-flex justify-content-center">
                <h1 class="name"><img src="@/assets/k.jpg" alt="K logo" class="k"><span class="eepr">eepr</span></h1>
            </div>
        </div>
        <div class="row">
            <div class="col-12 main">
                <form v-if="loginForm" @submit.prevent="loginUser">
                    <div class="col-12">
                        <input type="email" v-model="creds.email" placeholder=" Email" class="shadow">
                    </div>
                    <div class="col-12 mt-2">
                        <input type="password" v-model="creds.password" placeholder=" Password" class="shadow">
                    </div>
                    <div class="col-12 mt-3">
                        <button type="submit" class="btn btn-sm btn-outline-dark ml-2 shadow">Login</button>
                    </div>
                </form>
                <form v-else @submit.prevent="register">
                    <div class="col-12 mt-2">
                        <input type="text" v-model="newUser.username" placeholder=" Name" class="shadow">
                    </div>
                    <div class="col-12 mt-2">
                        <input type="email" v-model="newUser.email" placeholder=" Email" class="shadow">
                    </div>
                    <div class="col-12 mt-2">
                        <input type="password" v-model="newUser.password" placeholder=" Password" class="shadow">
                    </div>
                    <div class="col-12 mt-3"> <button type="submit"
                            class="btn btn-sm btn-outline-dark ml-2 shadow">Create Account</button>
                    </div>
                </form>
                <div @click="loginForm = !loginForm" class="mt-3">
                    <p v-if="loginForm" class="reg text-light">No account? Click to Register.</p>
                    <p v-else class="text-light reg">Already have an account? Click to Login</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import Navbar from '@/components/Navbar.vue'

    export default {
        name: "login",
        mounted() {
            this.$store.dispatch("authenticate");
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        },
        components: {
            Navbar
        }
    };
</script>

<style>
    .login {
        min-height: 100vh;
        background-image: linear-gradient(to right, #ff7300 30%, #ffc400);
    }

    .k {
        height: 3rem;
        width: 3rem;
        margin-bottom: 0.8rem !important;
    }

    .name {
        margin-top: 17vh;
        background-color: black;
        color: #ff7300;
        padding: 0rem 2.4rem;
        padding-top: 1rem;
        padding-bottom: 0.5rem;
        border-top-right-radius: 20px;
        border-bottom-left-radius: 20px;
    }

    .eepr {
        margin-left: -0.3rem;
    }

    .main {
        margin-top: 7vh;
    }

    .reg {
        cursor: pointer;
    }

    input {
        width: 15vw;
    }

    @media (max-width:768px) {
        .name {
            margin-top: 10vh;
        }

        input {
            width: 65vw !important;
        }

        .reg {
            font-size: 0.8rem;
        }

        .main {
            margin-top: 4vh;
        }
    }
</style>